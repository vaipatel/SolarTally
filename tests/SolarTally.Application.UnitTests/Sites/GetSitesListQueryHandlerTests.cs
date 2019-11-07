using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSitesList;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSitesListQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitesListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSitesListTest()
        {
            var handler = new GetSitesListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSitesListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SiteDetailsList>(result);
            // Check num sites
            Assert.Equal(1, result.SiteDetails.Count);
            Assert.Equal(1, result.Count);
            // Get the last site
            var lastSiteDto = result.SiteDetails.Last();
            Assert.Equal("PetroCanada Station", lastSiteDto.Name);
            // Check total power consumption
            Assert.Equal(2*(20 + 800 + 2000),
                lastSiteDto.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
