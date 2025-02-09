using Wolverine.Attributes;
using WolverineIssue.Messages;

namespace WolverineIssue.TestMessageHandlers;

[StickyHandler("queue1")]
public static class Message1Handler
{
    public static void Handle(TestMessage message)
    {
        Console.WriteLine($"Message1Handler");
    }
}