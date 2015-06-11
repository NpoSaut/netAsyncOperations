using System;

namespace AsyncOperations.Progress
{
    /// <summary>Помогает контролировать работу с <see cref="IProgressToken" />
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Проверяет на null-значение ссылку на <see cref="IProgressToken" /></item>
    ///         <item>Запускает выполняет метод Start при создании</item>
    ///         <item>Выполняет метод OnCompleated при вызове Dispose</item>
    ///     </list>
    /// </remarks>
    /// <example>
    ///     <code>
    ///         using (IProgressController progress = _progressControllerFactory.CreateController(ProgressToken))
    ///         {
    ///             ProgressToken.SetProgress(0.35);
    ///         }
    ///     </code>
    /// </example>
    public interface IProgressController : IDisposable
    {
        /// <summary>Устанавливает текущее значение прогресса операции</summary>
        /// <param name="Progress">Доля выполнения операции (0.0 - 1.0)</param>
        void SetProgress(Double Progress);

        /// <summary>Устанавливает Intermediate-значение прогресса операции</summary>
        void SetToIntermediate();

        /// <summary>Устанавливает сообщение с названием текущей операции</summary>
        /// <param name="Message">Описание текущей операции</param>
        void SetMessage(string Message);
    }

    public static class ProgressControllerHelper
    {
        /// <summary>Устанавливает Intermediate-значение прогресса операции и пользовательское сообщение</summary>
        /// <param name="Controller">Контроллер</param>
        /// <param name="Message">Описание операции</param>
        public static void SetToIntermediate(this IProgressController Controller, String Message)
        {
            Controller.SetToIntermediate();
            Controller.SetMessage(Message);
        }

        /// <summary>Устанавливает текущее значение прогресса операции</summary>
        /// <param name="Controller">Контроллер</param>
        /// <param name="Progress">Доля выполнения операции (0.0 - 1.0)</param>
        /// <param name="Message">Описание операции</param>
        public static void SetProgress(this IProgressController Controller, Double Progress, String Message)
        {
            Controller.SetProgress(Progress);
            Controller.SetMessage(Message);
        }
    }
}
