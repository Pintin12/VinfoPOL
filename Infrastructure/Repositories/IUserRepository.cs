using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username);
        void AddUser(User user);
        IEnumerable<User> GetAllUsers();
    }
}
