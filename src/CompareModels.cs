using Microsoft.Extensions.AI;

internal static class CompareModels
{
    public static async Task RunAsync()
    {
        string note = OpenAiSetup.ReadShiftNote();
        foreach (string name in new[] { "gpt-4o-mini", "gpt-3.5-turbo" })
        {
            IChatClient client = OpenAiSetup.CreateChatClient(name);
            ShiftBrief brief = await ShiftBriefChain.InvokeAsync(client, note);
            Console.WriteLine(name);
            Console.WriteLine(brief);
            Console.WriteLine("---");
        }
    }
}
