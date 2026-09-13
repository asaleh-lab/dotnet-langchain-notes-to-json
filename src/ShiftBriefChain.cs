using Microsoft.Extensions.AI;

internal static class ShiftBriefChain
{
    public static IReadOnlyList<ChatMessage> BuildMessages(string note) =>
    [
        new ChatMessage(
            ChatRole.System,
            $"Extract a shift brief. Reply with JSON only.\n{ShiftBrief.FormatInstructions}"),
        new ChatMessage(ChatRole.User, $"Shift note:\n{note}"),
    ];

    public static async Task<ShiftBrief> InvokeAsync(IChatClient client, string note)
    {
        ChatResponse response = await client.GetResponseAsync(
            BuildMessages(note),
            OpenAiSetup.ZeroTemperature);
        return ShiftBrief.Parse(response.Text);
    }
}
