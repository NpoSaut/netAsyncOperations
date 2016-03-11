using System;

namespace AsyncOperations.Progress
{
    public interface IDescriptionProvider
    {
        string GetDescription(IProgress Progress);
    }

    public class GlobalDescriptionProvider : IDescriptionProvider
    {
        private readonly string _format;
        public GlobalDescriptionProvider(string Format) { _format = Format; }
        public string GetDescription(IProgress Progress) { return String.Format(_format, Progress.Progress); }
    }

    public class LocalDescriptionProvider : IDescriptionProvider
    {
        private readonly string _format;
        private readonly IProgress _localProgress;

        public LocalDescriptionProvider(string Format, IProgress LocalProgress)
        {
            _format = Format;
            _localProgress = LocalProgress;
        }

        public string GetDescription(IProgress Progress) { return String.Format(_format, _localProgress.Progress, Progress.Progress); }
    }

    public static class DescriptionProviderHelper
    {
        public static void SetDescription(this IProgressController ProgressController, string Format)
        {
            ProgressController.SetDescription(new GlobalDescriptionProvider(Format));
        }

        public static void SetDescription(this IProgressController ProgressController, string Format, IProgress ProgressSource)
        {
            ProgressController.SetDescription(new LocalDescriptionProvider(Format, ProgressSource));
        }
    }
}
