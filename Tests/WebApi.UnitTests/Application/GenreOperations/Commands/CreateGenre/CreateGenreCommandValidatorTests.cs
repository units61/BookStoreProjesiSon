using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
       
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData("as")]
        [InlineData("asd")]
        [InlineData("a")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null!);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [Theory]
        [InlineData("asdf ")]
        [InlineData("asdf")]
        [InlineData("as123")]
        [InlineData("12asd")]
        [InlineData("    a")]
        public void WhenValidInputAreGiven_Validator_ShouldBeReturnErrors(string name)
        {
            //arrange
            CreateGenreCommand command = new CreateGenreCommand(null!);
            command.Model = new CreateGenreModel(){Name = name};
            
            //act
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().Be(0);
           
        }
       
    }
}