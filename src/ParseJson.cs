using Microsoft.Extensions.AI;

internal static class ParseJson
{
    public static async Task RunAsync()
    {
        string note = OpenAiSetup.ReadShiftNote();
        IChatClient client = OpenAiSetup.CreateChatClient("gpt-4o-mini");
        ShiftBrief brief = await ShiftBriefChain.InvokeAsync(client, note);
        Console.WriteLine(brief.GetType());
        Console.WriteLine(brief);
    }
}
