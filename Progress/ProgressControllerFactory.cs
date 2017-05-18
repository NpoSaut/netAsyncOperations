namespace AsyncOperations.Progress
{
    public class ProgressControllerFactory : IProgressControllerFactory
    {
        static ProgressControllerFactory() { Default = new ProgressControllerFactory(); }

        public static IProgressControllerFactory Default { get; }

        public IProgressController CreateController(IProgressToken Token) { return new ProgressController(Token); }
    }
}
