using Xunit;
using Domain.Entities;
using Infrastructure.Repositories;
using Application.Commands;
using Application.Handlers;

namespace VinfoPOLTests
{
    public class PostCommandHandlerTests
    {
        private readonly IUserRepository _repository;
        private readonly PostCommandHandler _handler;

        public PostCommandHandlerTests()
        {
            _repository = new InMemoryUserRepository();
            _repository.AddUser(new User("@Alfonso"));
            _handler = new PostCommandHandler(_repository);
        }

        [Fact]
        public void Handle_CreatesPost_WhenUserExists()
        {
            var command = new PostCommand("@Alfonso", "Mi Test");

            _handler.Handle(command);

            var user = _repository.GetUser("@Alfonso");
            Assert.NotNull(user);
            Assert.Single(user.Posts);
            Assert.Equal("Mi Test", user.Posts[0].Message);
        }

        [Fact]
        public void Handle_DoesNothing_WhenUserDoesNotExist()
        {
            var command = new PostCommand("@UsuarioInexistente", "Mensaje Inexistente");
            _handler.Handle(command);
            var noUser = _repository.GetUser("@UsuarioInexistente");
            Assert.Null(noUser);
        }
    }
}
