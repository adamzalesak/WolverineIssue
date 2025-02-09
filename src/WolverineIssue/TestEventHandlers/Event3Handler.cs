using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.TestEventHandlers;

public static class Event3Handler
{
    public static void Handle(TestEvent _)
    {
        Console.WriteLine("Event3 handled");
    }
}