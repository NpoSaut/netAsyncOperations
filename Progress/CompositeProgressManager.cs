using System;
using System.Collections.Generic;
using System.Linq;

namespace AsyncOperations.Progress
{
    public class CompositeProgressManager : IDisposable
    {
        private readonly double _completeWeight;
        private readonly IProgressToken _rootProgress;
        private readonly ICollection<SubprocessProgressToken> _subprocesses;
        private bool _compleated;
        private bool _started;

        public CompositeProgressManager(IProgressToken RootProgress, params SubprocessProgressToken[] Subprocesses)
            : this(RootProgress, (ICollection<SubprocessProgressToken>)Subprocesses) { }

        public CompositeProgressManager(IProgressToken RootProgress, ICollection<SubprocessProgressToken> Subprocesses)
        {
            if (RootProgress == null) return;

            _rootProgress = RootProgress;
            _subprocesses = Subprocesses;

            foreach (SubprocessProgressToken subprocess in Subprocesses)
            {
                subprocess.SetToIntermediate += SubprocessOnSetToIntermediate;
                subprocess.ProgressChanged += SubprocessOnProgressChanged;
                subprocess.Started += SubprocessOnStarted;
                subprocess.Compleated += SubprocessOnCompleated;
            }
            _completeWeight = _subprocesses.Sum(p => p.Weight);
        }

        public void Dispose()
        {
            if (!_compleated && _rootProgress != null)
                _rootProgress.OnCompleated();
        }

        private void SubprocessOnCompleated(object Sender, EventArgs Args)
        {
            if (_subprocesses.All(p => p.IsCompleated))
            {
                _compleated = true;
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

        private void SubprocessOnProgressChanged(object Sender, EventArgs Args)
        {
            _rootProgress.SetProgress(_subprocesses.Sum(p => p.Complete * p.Weight) / _completeWeight);
        }

        private void SubprocessOnSetToIntermediate(object Sender, EventArgs EventArgs) { _rootProgress.SetToIntermediate(); }
    }
}
