namespace AsyncOperations.Progress
{
    public class ProgressControllerFactory : IProgressControllerFactory
    {
        public IProgressController CreateController(IProgressToken Token) { return new ProgressController(Token); }
    }
}
