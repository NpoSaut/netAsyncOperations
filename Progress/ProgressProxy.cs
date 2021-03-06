﻿using System;

namespace AsyncOperations.Progress
{
    public class ProgressProxy : IProgressToken, IProgressPublisher
    {
        private IDescriptionProvider _descriptionProvider = new GlobalDescriptionProvider("{0:P0}");
        public event EventHandler Started;
        public event EventHandler Changed;
        public event EventHandler Compleated;

        public double Progress { get; private set; }
        public bool IsIntermediate { get; private set; }

        public string Description
        {
            get { return _descriptionProvider.GetDescription(this); }
        }

        public void Start()
        {
            Progress = 0;
            IsIntermediate = false;
            OnStarted();
        }

        public void SetProgress(double progress)
        {
            Progress = progress;
            IsIntermediate = false;
            OnChanged();
        }

        public void SetToIntermediate()
        {
            IsIntermediate = true;
            OnChanged();
        }

        public void OnCompleated()
        {
            EventHandler handler = Compleated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void SetDescription(IDescriptionProvider DescriptionProvider)
        {
            _descriptionProvider = DescriptionProvider;
            OnChanged();
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
