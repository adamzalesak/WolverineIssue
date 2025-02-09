using Wolverine.Attributes;
using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.TestMessageHandlers;

[StickyHandler("queue1")]
public static class Message1Handler
{
    public static void Handle(TestMessage message)
    {
        Console.WriteLine($"Message1Handler");
    }
}