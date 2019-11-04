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
        public async Task GetSitesTest()
        {
            var handler = new GetSitesListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSitesListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SitesListVm>(result);
            // Check num sites
            Assert.Equal(1, result.Sites.Count);
            Assert.Equal(1, result.Count);
            // Get the last site
            var lastSiteDto = result.Sites.Last();
            Assert.Equal("PetroCanada Station", lastSiteDto.Name);
            // Check power
            Assert.Equal(2*(20 + 800 + 2000), 
                lastSiteDto.Consumption.ConsumptionTotal.TotalPowerConsumption);
            // Check ApplianceUsages count
            Assert.Equal(3,
                result.Sites.Single().Consumption.ApplianceUsages.Count);
            // Get first ApplianceUsage
            var firstAU = lastSiteDto.Consumption.ApplianceUsages.First();
            // Check power consumption of the first ApplianceUsage
            Assert.Equal(20, firstAU.PowerConsumption);
        }

    }
}
