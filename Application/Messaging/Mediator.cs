using System;
using System.Collections.Generic;

namespace Application.Messaging
{
    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, object> _handlers = new Dictionary<Type, object>();

        public void RegisterHandler<T>(object handler)
        {
            _handlers[typeof(T)] = handler;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (_handlers.TryGetValue(typeof(TCommand), out var handler))
            {
                ((ICommandHandler<TCommand>)handler).Handle(command);
            }
            else
            {
                throw new Exception($"No se encontró handler para {typeof(TCommand).Name}");
            }
        }

        public TResult Send<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            if (_handlers.TryGetValue(typeof(TQuery), out var handler))
            {
                return ((IQueryHandler<TQuery, TResult>)handler).Handle(query);
            }
            else
            {
                throw new Exception($"No se encontró handler para {typeof(TQuery).Name}");
            }
        }
    }
}
