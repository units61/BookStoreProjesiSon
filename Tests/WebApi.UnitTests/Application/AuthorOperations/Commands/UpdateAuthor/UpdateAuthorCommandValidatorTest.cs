using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(0,"Lor","Asd")]
        [InlineData(0,"Lo ","ASDF ")]
        [InlineData(1,"Lord"," SD")]
        [InlineData(0,"Lor","ASDF")]
        [InlineData(-1,"Lord Of", " ")]
        [InlineData(1," "," ")]
        [InlineData(1,"","ASDF")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int authorid, string firstname, string lastname)
        {
            //arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel(){ FirstName=firstname, LastName=lastname};
            command.AuthorId=authorid;
            //act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
           
        }

        [InlineData(1,"Lord Of The Rings","ASDF")]
        [Theory]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorid, string firstname, string lastname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstname,
                LastName = lastname                
            };
            command.AuthorId=authorid;

            UpdateAuthorCommandValidator validations=new UpdateAuthorCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

      
    }

}