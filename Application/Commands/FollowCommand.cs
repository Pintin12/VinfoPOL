using Application.Messaging;

namespace Application.Commands
{
    public class FollowCommand : ICommand
    {
        public string SourceUsername { get; }
        public string TargetUsername { get; }

        public FollowCommand(string sourceUsername, string targetUsername)
        {
            SourceUsername = sourceUsername;
            TargetUsername = targetUsername;
        }
    }
}
