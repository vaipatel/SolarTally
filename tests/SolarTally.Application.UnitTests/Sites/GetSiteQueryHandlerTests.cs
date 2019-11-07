using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSite;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSiteQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSite()
        {
            var handler = new GetSiteQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSiteQuery()
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
