using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.UserOperations.GetUserDetail;

namespace Application.UserOperations.Queries
{
    public class GetUserDetailQueryTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenGivenUserIdIsNotinDB_InvalidOperationException_ShouldBeReturn()
        {
            GetUserDetailQuery command = new GetUserDetailQuery(_context,_mapper);
            command.UserId=0;
                        

            FluentActions.Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Kullanıcı bulunamadı...");
        }

        // [Fact]
        // public void WhenGivenUserIdIsinDB_ValidOperationException_ShouldBeReturn()
        // {
        //     GetUserDetailQuery command = new GetUserDetailQuery(_context,_mapper);
        //     command.UserId=1;
            

        //     FluentActions.Invoking(()=> command.Handle()).Invoke();

        //     var user=_context.Users.SingleOrDefault(user=>user.Id == command.UserId);
        //     user.Should().NotBeNull();
        // }
    }
}