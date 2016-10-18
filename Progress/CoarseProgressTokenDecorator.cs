using System;

namespace AsyncOperations.Progress
{
    public class CoarseProgressTokenDecorator : IProgressToken
    {
        private readonly IProgressToken _core;
        private readonly int _digits;
        private double _previous = -1;

        public CoarseProgressTokenDecorator(IProgressToken Core, int Digits = 2)
        {
            _core = Core;
            _digits = Digits;
        }

        public void Start() { _core.Start(); }

        public void SetProgress(double Progress)
        {
            double val = Math.Round(Progress, _digits);
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (val != _previous)
            {
                _core.SetProgress(Progress);
                _previous = val;
            }
        }

        public void SetToIntermediate() { _core.SetToIntermediate(); }
        public void OnCompleated() { _core.OnCompleated(); }
        public void SetDescription(IDescriptionProvider DescriptionProvider) { _core.SetDescription(DescriptionProvider); }
    }
}
