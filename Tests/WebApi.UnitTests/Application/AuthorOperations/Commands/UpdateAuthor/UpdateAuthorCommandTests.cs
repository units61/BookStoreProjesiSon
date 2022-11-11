
using FluentAssertions;
using TestSetup;
using WebApi.AuthorOperations.UpdateAuthor;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;


namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (Hazırlık)
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 0;

            // act & asset (Çalıştırma ve Doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek kitap bulunamadı...");

        }

        [Fact]
        public void WhenGivenAuthorIdinDB_Author_ShouldBeUpdate()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);

            UpdateAuthorModel model = new UpdateAuthorModel(){FirstName="WhenGivenBookIdinDB_Book_ShouldBeUpdate", LastName="Rebart"};            
            command.Model = model;
            command.AuthorId = 1;

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var author=_context.Authors.SingleOrDefault(author=>author.Id == command.AuthorId);
            author.Should().NotBeNull();
            
        }
    }

}