


using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            
        }

        [Fact]
        public void WhenAlreadyExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            DeleteBookCommand command = new DeleteBookCommand(_context);
            

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı ...");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
           var book = new Book() {Title="Lord Of The Rings", PageCount=100, PublishDate=new System.DateTime(1990,05,22), GenreId=1, AuthorId=1};
           _context.Add(book);
           _context.SaveChanges();

           DeleteBookCommand command = new DeleteBookCommand(_context);
           command.BookId = book.Id;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            book = _context.Books.SingleOrDefault(x=> x.Id == book.Id);
            book.Should().BeNull();

        }
    }

}