using WolverineOutboxIssue.Messages;

namespace WolverineOutboxIssue.AbcMessageHandlers;

// issue #3 - this handler is being ignored
public static class AbcHandler
{
    public static void Handle(AbcMessage message)
    {
        Console.WriteLine("AbcHandler");
    }
}