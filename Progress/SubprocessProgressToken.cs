using System;

namespace AsyncOperations.Progress
{
    public class SubprocessProgressToken : IProgressToken, IProgress
    {
        public SubprocessProgressToken(double Weight = 1.0)
        {
            Progress = 0;
            this.Weight = Weight;
        }

        public IDescriptionProvider DescriptionProvider { get; private set; }

        public Double Weight { get; private set; }
        public bool IsCompleated { get; private set; }
        public Double Progress { get; private set; }
        public bool IsIntermediate { get; private set; }

        public string Description
        {
            get { return DescriptionProvider.ToString(); }
        }

        void IProgressToken.Start() { OnStarted(); }

        void IProgressToken.SetToIntermediate()
        {
            IsIntermediate = true;
            OnSetToIntermediate();
        }

        void IProgressToken.SetProgress(double Value)
        {
            IsIntermediate = false;
            Progress = Value;
            OnProgressChanged();
        }

        void IProgressToken.OnCompleated()
        {
            IsIntermediate = false;
            Progress = 1.0;
            IsCompleated = true;
            OnCompleated();
        }

        void IProgressToken.SetDescription(IDescriptionProvider DescriptionProvider)
        {
            this.DescriptionProvider = DescriptionProvider;
            OnDescriptionChanged();
        }

        public event EventHandler Started;

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler SetToIntermediate;

        protected virtual void OnSetToIntermediate()
        {
            EventHandler handler = SetToIntermediate;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Compleated;

        protected virtual void OnCompleated()
        {
            EventHandler handler = Compleated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler ProgressChanged;

        protected virtual void OnProgressChanged()
        {
            EventHandler handler = ProgressChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler DescriptionChanged;

        protected virtual void OnDescriptionChanged()
        {
            EventHandler handler = DescriptionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
