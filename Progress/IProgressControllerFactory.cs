namespace AsyncOperations.Progress
{
    public interface IProgressControllerFactory
    {
        IProgressController CreateController(IProgressToken Token);
    }
}
