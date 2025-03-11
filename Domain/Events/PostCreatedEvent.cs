using Domain.Entities;
using System;


namespace Domain.Events
{
    public class PostCreatedEvent : IDomainEvent
    {
        public Post Post { get; }
        public DateTime OccurredOn { get; }

        public PostCreatedEvent(Post post)
        {
            Post = post;
            OccurredOn = DateTime.UtcNow;
        }
    }
}
