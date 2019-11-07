using AutoMapper;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSitePartialDtosList;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.Sites
{
    [Collection("QueryCollection")]
    public class GetSitePartialDtosListQueryHandlerTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitePartialDtosListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetSitePartialDtosTest()
        {
            var handler = new GetSitePartialDtosListQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetSitePartialDtosListQuery(),
                CancellationToken.None);
            
            Assert.IsType<SitePartialDtosListVm>(result);
            // Check num sites
            Assert.Equal(1, result.Sites.Count);
            Assert.Equal(1, result.Count);
            // Get the last site
            var lastSitePartialDto = result.Sites.Last();
            Assert.Equal("PetroCanada Station", lastSitePartialDto.Name);
            // Check total power consumption
            Assert.Equal(5640,
                lastSitePartialDto.ConsumptionTotal.TotalPowerConsumption);
        }

    }
}
