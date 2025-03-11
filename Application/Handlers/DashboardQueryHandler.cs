using Application.Messaging;
using Application.Queries;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Handlers
{
    public class DashboardQueryHandler : IQueryHandler<DashboardQuery, IEnumerable<Post>>
    {
        private readonly IUserRepository _userRepository;

        public DashboardQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<Post> Handle(DashboardQuery query)
        {
            var user = _userRepository.GetUser(query.Username);
            if (user == null)
            {
                Console.WriteLine($"Usuario {query.Username} no encontrado");
                return Enumerable.Empty<Post>();
            }

            List<Post> posts = new List<Post>();

            foreach (var followed in user.Following)
            {
                posts.AddRange(followed.Posts);
            }
            return posts.OrderBy(p => p.Timestamp);
        }
    }
}
