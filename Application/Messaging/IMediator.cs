namespace Application.Messaging
{
    public interface IMediator
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
        TResult Send<TQuery, TResult>(TQuery query) where TQuery : IQuery;
    }
}
