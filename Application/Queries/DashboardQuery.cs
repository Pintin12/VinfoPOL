using Application.Messaging;

namespace Application.Queries
{
    public class DashboardQuery : IQuery
    {
        public string Username { get; }

        public DashboardQuery(string username)
        {
            Username = username;
        }
    }
}
