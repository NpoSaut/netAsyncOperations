using System;
using AsyncOperations.Progress;

namespace AsyncOperations.OperationTokens
{
    public interface IAsyncOperationToken
    {
        IProgressPublisher Progress { get; }
        bool CanAbort { get; }

        void RunOnException(Action<Exception> ExceptionHandler);
        void RunWhenCompleated(Action<AsyncOperationCompleatingStatus> CompletionHandler);

        void Abort();
    }

    public static class AsyncOperationTokenHelper
    {
        public static void RunOnException<TException>(this IAsyncOperationToken Token, Action<TException> ExceptionHandler) where TException : Exception
        {
            Token.RunOnException(e =>
                                 {
                                     if (e is TException)
                                         ExceptionHandler((TException)e);
                                 });
        }
    }

    public enum AsyncOperationCompleatingStatus
    {
        Success,
        Error
    }
}
