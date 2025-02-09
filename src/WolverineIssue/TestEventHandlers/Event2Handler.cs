using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.TestEventHandlers;

public static class Event2Handler
{
    public static void Handle(TestEvent _)
    {
        Console.WriteLine("Event2 handled");
    }
}