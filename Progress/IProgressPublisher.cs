using System;

namespace AsyncOperations.Progress
{
    public interface IProgress
    {
        double Progress { get; }
        bool IsIntermediate { get; }
        string Description { get; }
    }

    public interface IProgressPublisher : IProgress
    {
        event EventHandler Started;
        event EventHandler Changed;
        event EventHandler Compleated;
    }
}
