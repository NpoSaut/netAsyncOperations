using System;

namespace AsyncOperations.Progress
{
    public class ProgressProxy : IProgressToken, IProgressPublisher
    {
        private readonly IProgressToken _next;
        private string _messageFormat = string.Empty;

        public ProgressProxy(IProgressToken Next = null) { _next = Next; }
        public String Message { get; private set; }

        public event EventHandler Started;
        public event EventHandler Changed;
        public event EventHandler Compleated;

        public double Progress { get; private set; }
        public bool IsIntermediate { get; private set; }

        public void Start()
        {
            Progress = 0;
            IsIntermediate = false;
            OnStarted();

            if (_next != null)
                _next.Start();
        }

        public void SetProgress(double progress)
        {
            Progress = progress;
            IsIntermediate = false;
            OnChanged();

            if (_next != null)
                _next.SetProgress(progress);
        }

        public void SetToIntermediate()
        {
            IsIntermediate = true;
            OnChanged();

            if (_next != null)
                _next.SetToIntermediate();
        }

        public void SetMessageFormat(string MessageFormat)
        {
            _messageFormat = MessageFormat;
            OnChanged();

            if (_next != null)
                _next.SetMessageFormat(MessageFormat);
        }

        public void OnCompleated()
        {
            EventHandler handler = Compleated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            Message = string.Format(_messageFormat, Progress);

            EventHandler handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
