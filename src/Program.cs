if (args is ["ChatOnce"])
{
    await ChatOnce.RunAsync();
    return;
}

if (args is ["PromptTemplate"])
{
    await PromptTemplate.RunAsync();
    return;
}

if (args is ["ParseJson"])
{
    await ParseJson.RunAsync();
    return;
}

if (args is ["App"])
{
    await App.RunAsync();
    return;
}

if (args is ["CompareModels"])
{
    await CompareModels.RunAsync();
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
