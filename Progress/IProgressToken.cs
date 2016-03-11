using System;

namespace AsyncOperations.Progress
{
    public interface IProgressToken
    {
        void Start();
        void SetProgress(Double Progress);
        void SetToIntermediate();
        void OnCompleated();
        void SetDescription(string descriptionFormat);
    }
}