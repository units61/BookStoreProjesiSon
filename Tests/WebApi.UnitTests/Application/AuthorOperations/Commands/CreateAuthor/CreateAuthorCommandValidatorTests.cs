using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.AuthorOperations.CreateAuthor;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(" ", " ")]
        [InlineData(" ", "asd" )]
        [InlineData("asd", " " )]
        [InlineData("as", "a" )]
        [InlineData("a", "sa" )]
        [InlineData("aaa", "saa" )]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel(){FirstName = firstname, LastName = lastname, BirthDay= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null!,null!);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Frank",
                LastName = "Tolkien",
                BirthDay = DateTime.Now.Date
                
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
            
        }

        [Theory]
        [InlineData("asdf ", " asdf")]
        [InlineData("asdf", "asdf" )]
        [InlineData("as  ", "sa  " )]
        [InlineData(" as ", " a  " )]
        [InlineData("asdadasdasd", "asdasdasdasdas" )]
        [InlineData(" aaa", "saa " )]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string firstname, string lastname)
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorModel(){FirstName = firstname, LastName = lastname, BirthDay= new System.DateTime(1900,01,25)};
            
            //act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        } 
    }
}