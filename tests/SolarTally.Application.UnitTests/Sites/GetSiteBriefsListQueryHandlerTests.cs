using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSiteBriefsList;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSiteBriefsListQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteBriefsListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSitesListTest()
        {
            var handler = new GetSiteBriefsListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSiteBriefsListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SiteBriefsListDto>(result);
            // Check num sites
            Assert.Equal(1, result.Items.Count);
            // Get the last site
            var siteBriefDto = result.Items.Last();
            Assert.Equal("PetroCanada Station", siteBriefDto.Name);
            // Check city
            Assert.Equal("Toronto", siteBriefDto.MainAddressCity);
            // Check total power consumption
            Assert.Equal(2*(20 + 800 + 2000),
                siteBriefDto.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
