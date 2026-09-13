using DotNetEnv;
using Microsoft.Extensions.AI;

internal static class OpenAiSetup
{
    public static string RequireApiKey()
    {
        if (File.Exists(".env"))
        {
            Env.Load();
        }

        var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")?.Trim();
        if (string.IsNullOrEmpty(apiKey))
        {
            Console.Error.WriteLine(
                "OPENAI_API_KEY is missing. Copy .env.example to .env and set your key.");
            Environment.Exit(1);
        }

        return apiKey;
    }

    public static IChatClient CreateChatClient(string model)
    {
        return new OpenAI.Chat.ChatClient(model, RequireApiKey()).AsIChatClient();
    }

    public static string ReadShiftNote()
    {
        return File.ReadAllText(Path.Combine("data", "shift_note.txt"));
    }

    public static ChatOptions ZeroTemperature { get; } = new() { Temperature = 0f };
}
