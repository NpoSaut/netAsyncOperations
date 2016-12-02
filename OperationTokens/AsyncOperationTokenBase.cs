using System;
using System.Collections.Generic;
using AsyncOperations.Progress;

namespace AsyncOperations.OperationTokens
{
    public class AsyncOperationTokenBase : IAsyncOperationToken
    {
        private readonly List<Action<AsyncOperationCompleatingStatus>> _completionHandlers = new List<Action<AsyncOperationCompleatingStatus>>();
        private readonly List<Action<Exception>> _exceptionHandlers = new List<Action<Exception>>();
        private readonly object _schedulingLocker = new object();
        private Exception _exception;
        private AsyncOperationCompleatingStatus? _status;

        public AsyncOperationTokenBase(IProgressPublisher Progress, bool CanAbort)
        {
            this.CanAbort = CanAbort;
            this.Progress = Progress;
        }

        public IProgressPublisher Progress { get; private set; }

        public void RunOnException(Action<Exception> ExceptionHandler)
        {
            lock (_schedulingLocker)
            {
                if (_exception != null)
                    ExceptionHandler(_exception);
                else
                    _exceptionHandlers.Add(ExceptionHandler);
            }
        }

        public void RunWhenCompleated(Action<AsyncOperationCompleatingStatus> CompletionHandler)
        {
            lock (_schedulingLocker)
            {
                if (_status.HasValue)
                    CompletionHandler(_status.Value);
                else
                    _completionHandlers.Add(CompletionHandler);
            }
        }

        public bool CanAbort { get; private set; }
        public virtual void Abort() { throw new NotImplementedException(); }

        protected virtual void OnCompleated(AsyncOperationCompleatingStatus CompletionStatus)
        {
            lock (_schedulingLocker)
            {
                _status = CompletionStatus;
                foreach (var handler in _completionHandlers)
                    handler(CompletionStatus);
            }
        }

        protected virtual void OnException(Exception HandledException)
        {
            lock (_schedulingLocker)
            {
                _exception = HandledException;
                foreach (var handler in _exceptionHandlers)
                    handler(HandledException);
                OnCompleated(AsyncOperationCompleatingStatus.Error);
            }
        }
    }
}
