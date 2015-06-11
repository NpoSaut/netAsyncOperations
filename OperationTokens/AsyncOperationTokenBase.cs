using System;
using AsyncOperations.Progress;

namespace AsyncOperations.OperationTokens
{
    public class AsyncOperationTokenBase : IAsyncOperationToken
    {
        public AsyncOperationTokenBase(IProgressPublisher Progress, bool CanAbort)
        {
            this.CanAbort = CanAbort;
            this.Progress = Progress;
        }

        public IProgressPublisher Progress { get; private set; }

        public event EventHandler<ExceptionHandledEventArgs> ExceptionHandled;
        public event EventHandler<AsyncOperationCompleatedEventArgs> Compleated;

        public bool CanAbort { get; private set; }
        public virtual void Abort() { throw new NotImplementedException(); }

        protected virtual void OnExceptionHandled(ExceptionHandledEventArgs E)
        {
            EventHandler<ExceptionHandledEventArgs> handler = ExceptionHandled;
            if (handler != null) handler(this, E);
        }

        protected virtual void OnCompleated(AsyncOperationCompleatedEventArgs E)
        {
            EventHandler<AsyncOperationCompleatedEventArgs> handler = Compleated;
            if (handler != null) handler(this, E);
        }
    }
}
