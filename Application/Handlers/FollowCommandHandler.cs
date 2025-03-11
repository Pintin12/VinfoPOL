using Application.Commands;
using Application.Messaging;
using Infrastructure.Repositories;
using System;

namespace Application.Handlers
{
    public class FollowCommandHandler : ICommandHandler<FollowCommand>
    {
        private readonly IUserRepository _userRepository;

        public FollowCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(FollowCommand command)
        {
            var source = _userRepository.GetUser(command.SourceUsername);
            var target = _userRepository.GetUser(command.TargetUsername);
            if (source == null)
            {
                Console.WriteLine($"Usuario {command.SourceUsername} no encontrado");
                return;
            }
            if (target == null)
            {
                Console.WriteLine($"No se encontró ningún usuario {command.TargetUsername}");
                return;
            }
            if (source.Follow(target))
                Console.WriteLine($"{source.Username} empezó a seguir a {target.Username}");
            else
                Console.WriteLine($"{source.Username} ya está siguiendo a {target.Username}");
        }
    }
}
