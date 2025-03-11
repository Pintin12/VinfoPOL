using Application.Messaging;

namespace Application.Commands
{
    public class PostCommand : ICommand
    {
        public string Username { get; }
        public string Message { get; }

        public PostCommand(string username, string message)
        {
            Username = username;
            Message = message;
        }
    }
}
