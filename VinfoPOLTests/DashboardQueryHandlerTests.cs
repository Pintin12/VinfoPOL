using Xunit;
using Domain.Entities;
using Infrastructure.Repositories;
using Application.Queries;
using Application.Handlers;
using System.Linq;
using System.Collections.Generic;

namespace VinfoPOLTests
{
    public class DashboardQueryHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly DashboardQueryHandler _handler;

        public DashboardQueryHandlerTests()
        {
            _repository = new InMemoryUserRepository();
            _repository.AddUser(new User("@Alicia"));
            _repository.AddUser(new User("@Ivan"));
            _repository.AddUser(new User("@Alfonso"));

            _handler = new DashboardQueryHandler(_repository);
        }

        [Fact]
        public void Handle_ReturnsEmpty_WhenUserNotFound()
        {
            var query = new DashboardQuery("@UsuarioInexistente");
            var result = _handler.Handle(query);
            Assert.Empty(result);
        }

        [Fact]
        public void Handle_ReturnsEmpty_WhenUserFollowsNoOne()
        {
            
            var query = new DashboardQuery("@Alicia");
            var result = _handler.Handle(query);
            Assert.Empty(result);
        }

        [Fact]
        public void Handle_ReturnsPostsFromFollowedUsers()
        {
            var alicia = _repository.GetUser("@Alicia");
            var ivan = _repository.GetUser("@Ivan");
            var alfonso = _repository.GetUser("@Alfonso");

            ivan.AddPost(new Post(ivan, "Post de Ivan"));
            alfonso.AddPost(new Post(alfonso, "Post de Alfonso"));

            alicia.Follow(ivan);

            var query = new DashboardQuery("@Alicia");

            IEnumerable<Post> result = _handler.Handle(query);

            var postsList = result.ToList();
            Assert.Single(postsList);
            Assert.Equal("Post de Ivan", postsList[0].Message);
        }
    }
}
