using Domain.Entities;
using System;
using System.Collections.Generic;


namespace Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>(StringComparer.OrdinalIgnoreCase);

        public User GetUser(string username)
        {
            _users.TryGetValue(username, out var user);
            return user;
        }

        public void AddUser(User user) => _users[user.Username] = user;

        public IEnumerable<User> GetAllUsers() => _users.Values;
    }
}
