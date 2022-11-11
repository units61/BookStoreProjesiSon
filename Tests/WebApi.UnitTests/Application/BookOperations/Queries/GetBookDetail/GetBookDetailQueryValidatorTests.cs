using FluentAssertions;
using TestSetup;
using WebApi.BookOperations.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests:IClassFixture<CommonTestFixture>
    {

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-10)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookid;

            GetBookDetailQeuryValidator validator = new GetBookDetailQeuryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [InlineData(1)]
        [InlineData(100)]
        [Theory]
        public void WhenInvalidBookidIsGiven_Validator_ShouldNotBeReturnErrors(int bookid)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null,null);
            query.BookId=bookid;

            GetBookDetailQeuryValidator validator = new GetBookDetailQeuryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }


    }
}