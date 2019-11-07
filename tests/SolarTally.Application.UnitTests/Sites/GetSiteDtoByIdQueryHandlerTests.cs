using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSiteDtoById;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSiteDtoByIdQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteDtoByIdQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSiteDtoByIdTest()
        {
            var handler = new GetSiteDtoByIdQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSiteDtoByIdQuery()
            { Id = 1 }, CancellationToken.None);
            
            Assert.IsType<SiteDto>(result);
            // Get the site name
            Assert.Equal("PetroCanada Station", result.Name);
            // Check power
            Assert.Equal(2*(20 + 800 + 2000), 
                result.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
