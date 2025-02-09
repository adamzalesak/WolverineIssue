using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.TestMessageHandlers;

public static class Message3Handler
{
    public static void Handle(TestMessage message)
    {
        Console.WriteLine($"Message3Handler");
    }
}