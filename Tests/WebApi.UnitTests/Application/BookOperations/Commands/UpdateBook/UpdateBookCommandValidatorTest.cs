using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(1,"Lor",1)]
        [InlineData(0,"Lord",1)]
        [InlineData(1,"Lord O",-1)]
        [InlineData(0,"Lor",0)]
        [InlineData(-1,"Lord Of",-1)]
        [InlineData(1," ",1)]
        [InlineData(1,"",1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int bookid, string title, int genreId)
        {
            //arrange
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel(){ Title=title, GenreId=genreId};
            command.BookId=bookid;
            //act
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [InlineData(1,1,"Lord Of The Rings")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookid, int genreid, string title)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.Model = new UpdateBookModel()
            {
                Title =title,
                GenreId = genreid                
            };
            command.BookId=bookid;

            UpdateBookCommandValidator validations=new UpdateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

      
    }

}