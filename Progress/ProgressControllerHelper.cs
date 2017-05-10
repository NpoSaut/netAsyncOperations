namespace AsyncOperations.Progress
{
    public static class ProgressControllerHelper
    {
        /// <summary>Устанавливает Intermediate-значение прогресса операции с описанием</summary>
        /// <param name="Controller">Контроллер прогресса</param>
        /// <param name="MessageFormat">Формат сообщения</param>
        public static void SetToIntermediate(this IProgressController Controller, string MessageFormat)
        {
            Controller.SetToIntermediate();
            Controller.SetDescription(MessageFormat);
        }
    }
}
