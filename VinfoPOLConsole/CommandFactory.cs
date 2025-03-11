using Application.Commands;
using Application.Queries;
using System;

namespace VinfoPOLConsole
{
    public static class CommandFactory
    {
        public static object Create(string input)
        {
            string[] parts = input.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return null;

            string command = parts[0].ToLower();
            switch (command)
            {
                case "post":
                    if (parts.Length < 3)
                        return null;
                    return new PostCommand(parts[1], parts[2]);

                case "follow":
                    string[] followParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (followParts.Length != 3)
                        return null;
                    return new FollowCommand(followParts[1], followParts[2]);

                case "dashboard":
                    if (parts.Length != 2)
                        return null;
                    return new DashboardQuery(parts[1]);

                default:
                    return null;
            }
        }
    }
}
