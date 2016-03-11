using System;

namespace AsyncOperations.Progress
{
    public class SubprocessProgressToken : IProgressToken
    {
        public SubprocessProgressToken(double Weight = 1.0)
        {
            Complete = 0;
            this.Weight = Weight;
        }

        public Double Complete { get; private set; }
        public Double Weight { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleated { get; private set; }

        void IProgressToken.Start() { OnStarted(); }
        void IProgressToken.SetToIntermediate() { OnSetToIntermediate(); }

        void IProgressToken.SetProgress(double Progress)
        {
            Complete = Progress;
            OnProgressChanged();
        }

        void IProgressToken.OnCompleated()
        {
            Complete = 1.0;
            IsCompleated = true;
            OnCompleated();
        }

        void IProgressToken.SetDescription(string DescriptionFormat)
        {
            Description = DescriptionFormat;
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
