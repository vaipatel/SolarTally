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
            
            Assert.IsType<SitesListDto>(result);
            // Check num sites
            Assert.Equal(1, result.Sites.Count);
            Assert.Equal(1, result.Count);
            // Get the last site
            var lastSiteDto = result.Sites.Last();
            Assert.Equal("PetroCanada Station", lastSiteDto.Name);
            // Check city
            Assert.Equal("Toronto", lastSiteDto.MainAddress.City);
            // Check num solar hours
            Assert.Equal(7, lastSiteDto.NumSolarHours);
            // Check total power consumption
            Assert.Equal(2*(20 + 800 + 2000),
                lastSiteDto.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
