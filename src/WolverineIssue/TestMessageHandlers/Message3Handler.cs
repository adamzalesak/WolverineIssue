using WolverineIssue.Messages;

namespace WolverineIssue.TestMessageHandlers;

public static class Message3Handler
{
    public static void Handle(TestMessage message)
    {
        Console.WriteLine($"Message3Handler");
    }
}