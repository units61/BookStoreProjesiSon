


using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.CreateAuthor;
using WebApi.AuthorOperations.DeleteAuthor;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.AuthorOperations.CreateAuthor.CreateAuthorCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
       

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }


        [Fact]
        public void WhenGivenAuthorIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 0;

            FluentActions.Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek yazar bulunamadı ...");
        }


        [Fact]
        public void WhenGivenBookIdNotEqualAuthorId_InvalidOperationException_ShouldBeReturn()
        {
            
            var command1 = new DeleteAuthorModel() {BookId=0};

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

           FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın kitapları mevcut olduğundan silinemez ...");
        }


        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
           var author = new Author() {FirstName="Frank", LastName="Rebart", Birthday=new System.DateTime(1990,05,22)};
           _context.Add(author);
           _context.SaveChanges();

           DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
           command.AuthorId = author.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            author = _context.Authors.SingleOrDefault(x=> x.Id == author.Id);
            author.Should().BeNull();

        }
    }

}