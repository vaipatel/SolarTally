using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSiteById;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSiteByIdQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteByIdQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSiteByIdTest()
        {
            var handler = new GetSiteByIdQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSiteByIdQuery() { Id = 1 },
                CancellationToken.None);
            
            Assert.IsType<SiteDto>(result);
            // Get the site name
            Assert.Equal("PetroCanada Station", result.Name);
            // Check power
            Assert.Equal(2*(20 + 800 + 2000), 
                result.Consumption.ConsumptionTotal.TotalPowerConsumption);
            // Check ApplianceUsages count
            Assert.Equal(3, result.Consumption.ApplianceUsages.Count);
            // Get first ApplianceUsage
            var firstAU = result.Consumption.ApplianceUsages.First();
            // Check power consumption of the first ApplianceUsage
            Assert.Equal(20, firstAU.PowerConsumption);
        }

    }
}