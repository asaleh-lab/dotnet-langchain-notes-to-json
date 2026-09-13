if (args is ["ChatOnce"])
{
    await ChatOnce.RunAsync();
    return;
}

if (args.Length == 0)
{
    Console.Error.WriteLine(
        "Usage: dotnet run --project src -- <ChatOnce|PromptTemplate|ParseJson|App|CompareModels>");
    Environment.Exit(1);
}

Console.Error.WriteLine($"Unknown command: {args[0]}");
Environment.Exit(1);
