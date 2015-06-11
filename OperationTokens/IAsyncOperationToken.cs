using System;
using AsyncOperations.Progress;

namespace AsyncOperations.OperationTokens
{
    public interface IAsyncOperationToken
    {
        IProgressPublisher Progress { get; }
        bool CanAbort { get; }
        event EventHandler<ExceptionHandledEventArgs> ExceptionHandled;
        event EventHandler<AsyncOperationCompleatedEventArgs> Compleated;

        void Abort();
    }

    public enum AsyncOperationCompleatingStatus
    {
        Success,
        Error
    }

    public class AsyncOperationCompleatedEventArgs : EventArgs
    {
        public AsyncOperationCompleatedEventArgs(AsyncOperationCompleatingStatus Status) { this.Status = Status; }
        public AsyncOperationCompleatingStatus Status { get; private set; }
    }

    public class ExceptionHandledEventArgs : EventArgs
    {
        public ExceptionHandledEventArgs(Exception HandledException) { this.HandledException = HandledException; }
        public Exception HandledException { get; private set; }
    }
}
