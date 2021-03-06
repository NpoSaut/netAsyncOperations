﻿namespace AsyncOperations.Progress
{
    public class ProgressController : IProgressController
    {
        private readonly IProgressToken _token;

        public ProgressController(IProgressToken Token)
        {
            _token = Token;
            if (_token != null)
            {
                _token.Start();
                _token.SetToIntermediate();
            }
        }

        /// <summary>Устанавливает Intermediate-значение прогресса операции</summary>
        public void SetToIntermediate()
        {
            if (_token != null)
                _token.SetToIntermediate();
        }

        /// <summary>Устанавливает текущее значение прогресса операции</summary>
        /// <param name="Progress">Доля выполнения операции (0.0 - 1.0)</param>
        public void SetProgress(double Progress)
        {
            if (_token != null)
                _token.SetProgress(Progress);
        }

        /// <summary>Устанавливает описание производимой операции</summary>
        public void SetDescription(IDescriptionProvider Description)
        {
            if (_token != null)
                _token.SetDescription(Description);
        }

        public void Dispose()
        {
            if (_token != null)
                _token.OnCompleated();
        }
    }
}
