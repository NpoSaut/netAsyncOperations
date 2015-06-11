using System;

namespace AsyncOperations.Progress
{
    public interface IProgressPublisher
    {
        event EventHandler Started;
        event EventHandler Changed;
        event EventHandler Compleated;

        double Progress { get; }
        bool IsIntermediate { get; }
    }
}