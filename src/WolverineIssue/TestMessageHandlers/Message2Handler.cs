using Wolverine.Attributes;
using WolverineIssue.Messages;

namespace WolverineIssue.TestMessageHandlers;

[StickyHandler("queue1")]
public static class Message2Handler
{
    public static void Handle(TestMessage message)
    {
        Console.WriteLine($"Message2Handler");
    }
}