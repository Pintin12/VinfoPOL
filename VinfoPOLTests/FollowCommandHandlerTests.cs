using Xunit;
using Domain.Entities;
using Infrastructure.Repositories;
using Application.Commands;
using Application.Handlers;

namespace VinfoPOLTests
{
    public class FollowCommandHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly FollowCommandHandler _handler;

        public FollowCommandHandlerTests()
        {
            _repository = new InMemoryUserRepository();
            _repository.AddUser(new User("@Alicia"));
            _repository.AddUser(new User("@Ivan"));
            _repository.AddUser(new User("@Alfonso"));

            _handler = new FollowCommandHandler(_repository);
        }

        [Fact]
        public void Handle_AddsFollowing_WhenBothUsersExist()
        {
            var command = new FollowCommand("@Alicia", "@Ivan");
            _handler.Handle(command);
            
            var alicia = _repository.GetUser("@Alicia");
            var ivan = _repository.GetUser("@Ivan");
            
            Assert.NotNull(alicia);
            Assert.NotNull(ivan);
            Assert.Contains(ivan, alicia.Following);
        }

        [Fact]
        public void Handle_DoesNothing_WhenSourceUserDoesNotExist()
        {
            var command = new FollowCommand("@UsuarioInexistente", "@Ivan");
            _handler.Handle(command);
            
            var noUser = _repository.GetUser("@UsuarioInexistente");
            Assert.Null(noUser); 
            var ivan = _repository.GetUser("@Ivan");
            Assert.NotNull(ivan);
        }

        [Fact]
        public void Handle_DoesNothing_WhenTargetUserDoesNotExist()
        {
            var command = new FollowCommand("@Alicia", "@NoUser");
            _handler.Handle(command);
            
            var alicia = _repository.GetUser("@Alicia");
            Assert.NotNull(alicia);
            Assert.Empty(alicia.Following); 
        }

        [Fact]
        public void Handle_DoesNotDuplicate_WhenAlreadyFollowing()
        {
            var command = new FollowCommand("@Alicia", "@Ivan");

            _handler.Handle(command);
            _handler.Handle(command); 

            var alicia = _repository.GetUser("@Alicia");
            Assert.NotNull(alicia);
            Assert.Single(alicia.Following); 
        }
    }
}
