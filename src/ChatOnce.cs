using Microsoft.Extensions.AI;

internal static class ChatOnce
{
    public static async Task RunAsync()
    {
        IChatClient client = OpenAiSetup.CreateChatClient("gpt-4o-mini");
        ChatResponse response = await client.GetResponseAsync(
            "Reply with one short sentence: what is a kitchen shift note?",
            OpenAiSetup.ZeroTemperature);
        Console.WriteLine(response.Text);
    }
}
