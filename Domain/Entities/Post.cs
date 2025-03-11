using System;

namespace Domain.Entities
{
    public class Post
    {
        public User Author { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public Post(User author, string message)
        {
            Author = author;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
