using Domain.Events;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public string Username { get; }
        public List<Post> Posts { get; }
        public HashSet<User> Following { get; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public User(string username)
        {
            Username = username;
            Posts = new List<Post>();
            Following = new HashSet<User>();
        }

        private void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
            AddDomainEvent(new PostCreatedEvent(post));
        }

        public bool Follow(User user)
        {
            if (Following.Contains(user))
                return false;
            Following.Add(user);
            return true;
        }
    }
}
