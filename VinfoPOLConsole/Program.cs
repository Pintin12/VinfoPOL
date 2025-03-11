using Application.Commands;
using Application.Handlers;
using Application.Messaging;
using Application.Queries;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;

namespace VinfoPOLConsole
{
    class Program
    {
        static void Main()
        {
            IUserRepository userRepository = new InMemoryUserRepository();
            
            userRepository.AddUser(new User("@Alfonso"));
            userRepository.AddUser(new User("@Ivan"));
            userRepository.AddUser(new User("@Alicia"));

            var mediator = new Mediator();
            mediator.RegisterHandler<PostCommand>(new PostCommandHandler(userRepository));
            mediator.RegisterHandler<FollowCommand>(new FollowCommandHandler(userRepository));
            mediator.RegisterHandler<DashboardQuery>(new DashboardQueryHandler(userRepository));

            Console.WriteLine("Bienvenido a VinfoPOL");
            Console.WriteLine("Ingrese un comando:");

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                var message = CommandFactory.Create(input);
                if (message == null)
                {
                    Console.WriteLine("Comando no reconocido o formato inválido.");
                    continue;
                }

                if (message is PostCommand postCommand)
                {
                    mediator.Send(postCommand);
                }
                else if (message is FollowCommand followCommand)
                {
                    mediator.Send(followCommand);
                }
                else if (message is DashboardQuery dashboardQuery)
                {
                    IEnumerable<Post> posts = mediator.Send<DashboardQuery, IEnumerable<Post>>(dashboardQuery);
                    if (!posts.Any())
                    {
                        Console.WriteLine("No hay posts para mostrar.");
                    }
                    foreach (var post in posts)
                    {
                        Console.WriteLine($"\"{post.Message}\" @{post.Author.Username} @{post.Timestamp:HH:mm}");
                    }
                }
                else
                {
                    Console.WriteLine("Comando no reconocido o formato inválido.");
                }
            }
        }
    }
}
