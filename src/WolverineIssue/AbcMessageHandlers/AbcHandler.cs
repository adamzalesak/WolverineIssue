using WolverineIssue.Messages;

namespace WolverineIssue.AbcMessageHandlers;

// issue #2 - this handler is being ignored
public static class AbcHandler
{
    public static void Handle(AbcMessage message)
    {
        Console.WriteLine("AbcHandler");
    }
}