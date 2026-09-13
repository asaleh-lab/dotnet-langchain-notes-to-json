using Microsoft.Extensions.AI;

internal static class PromptTemplate
{
    public static async Task RunAsync()
    {
        string note = OpenAiSetup.ReadShiftNote();
        const string system = "You extract people and tasks from kitchen shift notes. Be terse.";
        string human = $"Shift note:\n{note}\n\nList each person and what they need to do.";

        ChatMessage[] messages =
        [
            new ChatMessage(ChatRole.System, system),
            new ChatMessage(ChatRole.User, human),
        ];

        Console.WriteLine("Filled prompt:");
        Console.WriteLine($"System: {system}");
        Console.WriteLine($"Human: {human}");
        Console.WriteLine("---");

        IChatClient client = OpenAiSetup.CreateChatClient("gpt-4o-mini");
        ChatResponse response = await client.GetResponseAsync(messages, OpenAiSetup.ZeroTemperature);
        Console.WriteLine(response.Text);
    }
}
