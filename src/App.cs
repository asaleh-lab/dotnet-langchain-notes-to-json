using System.Net;
using System.Text;
using Microsoft.Extensions.AI;

internal static class App
{
    public static async Task RunAsync()
    {
        IChatClient client = OpenAiSetup.CreateChatClient("gpt-4o-mini");
        string sample = OpenAiSetup.ReadShiftNote();

        using var listener = new HttpListener();
        listener.Prefixes.Add("http://127.0.0.1:5000/");
        listener.Start();
        Console.WriteLine("Serving on http://127.0.0.1:5000");

        while (true)
        {
            HttpListenerContext context = await listener.GetContextAsync();
            await HandleAsync(context, client, sample);
        }
    }

    private static async Task HandleAsync(
        HttpListenerContext context,
        IChatClient client,
        string sample)
    {
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        string? path = request.Url?.AbsolutePath;

        try
        {
            if (path != "/")
            {
                await WriteTextAsync(response, 404, "text/plain; charset=utf-8", "Not found");
                return;
            }

            string note = sample;
            ShiftBrief? brief = null;
            if (string.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                string body = await reader.ReadToEndAsync();
                note = ReadFormField(body, "note") ?? "";
                brief = await ShiftBriefChain.InvokeAsync(client, note);
            }

            await WriteTextAsync(response, 200, "text/html; charset=utf-8", RenderPage(note, brief));
        }
        catch (Exception ex)
        {
            await WriteTextAsync(response, 500, "text/plain; charset=utf-8", ex.Message);
        }
    }

    private static string RenderPage(string note, ShiftBrief? brief)
    {
        string briefBlock = brief is null ? "" : $"<pre>{EscapeHtml(brief.ToString())}</pre>";
        return $"""
            <!DOCTYPE html>
            <html lang="en">
            <head>
              <meta charset="utf-8">
              <title>Shift notes</title>
            </head>
            <body>
            <form method="post">
              <textarea name="note" rows="8" cols="60">{EscapeHtml(note)}</textarea><br>
              <button>Parse</button>
            </form>
            {briefBlock}
            </body>
            </html>
            """;
    }

    private static string EscapeHtml(string value) =>
        value
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;");

    private static string? ReadFormField(string body, string name)
    {
        foreach (string pair in body.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] parts = pair.Split('=', 2);
            if (parts.Length == 2 && Uri.UnescapeDataString(parts[0].Replace("+", " ")) == name)
            {
                return Uri.UnescapeDataString(parts[1].Replace("+", " "));
            }
        }

        return null;
    }

    private static async Task WriteTextAsync(
        HttpListenerResponse response,
        int statusCode,
        string contentType,
        string body)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(body);
        response.StatusCode = statusCode;
        response.ContentType = contentType;
        response.ContentLength64 = bytes.Length;
        await response.OutputStream.WriteAsync(bytes);
        response.Close();
    }
}
