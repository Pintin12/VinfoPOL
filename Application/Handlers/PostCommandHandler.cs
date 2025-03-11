using Application.Commands;
using Application.Messaging;
using Domain.Entities;
using Domain.Events;
using Infrastructure.Repositories;
using System;

namespace Application.Handlers
{
    public class PostCommandHandler : ICommandHandler<PostCommand>
    {
        private readonly IUserRepository _userRepository;

        public PostCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(PostCommand command)
        {
            var user = _userRepository.GetUser(command.Username);
            if (user == null)
            {
                Console.WriteLine($"Usuario {command.Username} no encontrado");
                return;
            }

            var post = new Post(user, command.Message);
            user.AddPost(post);
            
            foreach (var domainEvent in user.DomainEvents)
            {
                if (domainEvent is PostCreatedEvent pce)
                {
                    Console.WriteLine($"Post creado por {pce.Post.Author.Username} " +
                                      $"a las {pce.OccurredOn} diciendo: {pce.Post.Message}");
                }
            }

            user.ClearDomainEvents();
        }
    }
}
