using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AsyncOperations.Progress
{
    public class CompositeProgressManager : IDisposable
    {
        private readonly double _completeWeight;
        private readonly IProgressToken _rootProgress;
        private readonly ICollection<Subprogress> _subprogresses;
        private int _compleated;
        private bool _compleatedPublished;
        private bool _started;

        public CompositeProgressManager(IProgressToken RootProgress, params Subprogress[] Subprocesses)
            : this(RootProgress, (ICollection<Subprogress>)Subprocesses) { }

        public CompositeProgressManager(IProgressToken RootProgress, ICollection<Subprogress> Subprogresses)
        {
            if (RootProgress == null) return;

            _rootProgress = RootProgress;
            _subprogresses = Subprogresses;

            foreach (Subprogress subprocess in Subprogresses)
            {
                subprocess.Progress.Changed += SubprogressOnChanged;
                subprocess.Progress.Started += SubprocessOnStarted;
                subprocess.Progress.Compleated += SubprocessOnCompleated;
            }
            _completeWeight = _subprogresses.Sum(p => p.Weight);
        }

        public void Dispose()
        {
            if (!_compleatedPublished && _rootProgress != null)
                _rootProgress.OnCompleated();
        }

        private void SubprogressOnChanged(object Sender, EventArgs Args)
        {
            var progr = (IProgressPublisher)Sender;
            if (progr.IsIntermediate)
                _rootProgress.SetToIntermediate();
            else
                _rootProgress.SetProgress(_subprogresses.Sum(p => p.Progress.Progress * p.Weight) / _completeWeight);
        }

        private void SubprocessOnCompleated(object Sender, EventArgs Args)
        {
            if (Interlocked.Increment(ref _compleated) == _subprogresses.Count - 1)
            {
                _compleatedPublished = true;
                _rootProgress.OnCompleated();
            }
        }

        private void SubprocessOnStarted(object Sender, EventArgs Args)
        {
            if (!_started)
            {
                _started = true;
                _rootProgress.Start();
            }
        }
    }

    public class Subprogress
    {
        public Subprogress(double Weight, IProgressPublisher Progress)
        {
            this.Weight = Weight;
            this.Progress = Progress;
        }

        public Double Weight { get; private set; }
        public IProgressPublisher Progress { get; private set; }
    }
}
