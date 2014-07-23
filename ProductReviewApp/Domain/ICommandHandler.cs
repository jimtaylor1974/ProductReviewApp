namespace ProductReviewApp.Domain
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : class, new()
    {
        TResult Dispatch(TCommand command);
    }
}