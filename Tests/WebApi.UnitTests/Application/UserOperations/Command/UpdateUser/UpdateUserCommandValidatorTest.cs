using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UserOperations.UpdateUser;

namespace Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateUserCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0,"","","","")]
        [InlineData(0,"asd","","","1234")]
        [InlineData(1,"as","s","a","12345")]
        [InlineData(0,"asdf","asd","adsf","12")]
        [InlineData(0,"asd","adff","asd","123")]
        [InlineData(0,"as","as","asdf","3")]
        [InlineData(0,"asd","asdf","asdf","123456")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int userid, string name, string surname, string email, string password)
        {
            //arrange
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model = new UpdateUserModel(){ Name=name, Surname=surname, Email=email, Password=password };
            command.UserId = userid;
            //act
            UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [InlineData(1,"asdf","asdff","asdff","123456")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int userid, string name, string surname, string email, string password)
        {
            UpdateUserCommand command = new UpdateUserCommand(null);
            command.Model = new UpdateUserModel()
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password               
            };
            command.UserId=userid;

            UpdateUserCommandValidator validations=new UpdateUserCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }

}