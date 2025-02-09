using Wolverine.Attributes;
using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.TestEventHandlers;

[StickyHandler("event1")]
public static class Event1Handler
{
    public static void Handle(TestEvent _)
    {
        Console.WriteLine("Event1 handled");
    }
}