using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.CreateAuthor;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UserOperations.CreateUser;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;
using static WebApi.UserOperations.CreateUser.CreateUserCommand;

namespace Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistUserEmailIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            var user = new User() {Name = "Ahmet", Surname="Adıvar", Email="ahmetadivar@mail.com", Password="123456" };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = new CreateUserModel() {Email = user.Email};
            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı zaten mevcut");

        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            //arrange
            CreateUserCommand command = new CreateUserCommand(_context,_mapper);
            CreateUserModel model = new CreateUserModel() {Name = "Ahmet", Surname="Adıvar", Email="ahmetadivar@mail.com", Password="123456", RefreshToken="asdasdasds" };
            command.Model = model;
            
            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var user = _context.Users.SingleOrDefault(user => user.Name == model.Name && user.Surname == model.Surname && user.Email == model.Email && user.Password == model.Password);
            user.Should().NotBeNull();
            
        }

    

        
    }

}