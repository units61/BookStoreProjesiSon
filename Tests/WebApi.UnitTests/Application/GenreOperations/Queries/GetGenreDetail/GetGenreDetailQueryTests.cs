using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries;
using WebApi.BookOperations.GetBookDetail;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetGenreDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenGenreIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context,_mapper);
            command.GenreId=0;
                        

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenGivenGenreIdIsinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery command = new GetGenreDetailQuery(_context,_mapper);
            command.GenreId=1;
            

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genre =_context.Books.SingleOrDefault(genre=>genre.Id == command.GenreId);
            genre.Should().NotBeNull();
        }
    }
}