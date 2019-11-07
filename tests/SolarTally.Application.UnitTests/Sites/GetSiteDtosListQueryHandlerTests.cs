using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSiteDtosList;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSiteDtosListQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteDtosListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSiteDtosTest()
        {
            var handler = new GetSiteDtosListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSiteDtosListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SiteDtosListVm>(result);
            // Check num sites
            Assert.Equal(1, result.SiteDtos.Count);
            Assert.Equal(1, result.Count);
            // Get the last site
            var lastSitePartialDto = result.SiteDtos.Last();
            Assert.Equal("PetroCanada Station", lastSitePartialDto.Name);
            // Check total power consumption
            Assert.Equal(2*(20 + 800 + 2000),
                lastSitePartialDto.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
