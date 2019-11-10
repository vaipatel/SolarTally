using AutoMapper;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesById;
using SolarTally.Application.UnitTests.Common;
using SolarTally.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SolarTally.Application.UnitTests.ApplianceUsages
{
    [Collection("QueryCollection")]
    public class GetApplianceUsageQueryTests
    {
        private readonly SolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetApplianceUsageQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetApplianceUsagesByIdTest()
        {
            var handler = new GetApplianceUsagesByIdQueryHandler(
                _context, _mapper);

            var result = await handler.Handle(new GetApplianceUsagesByIdQuery()
                { ConsumptionId = 1 }, CancellationToken.None);
            
            Assert.IsType<ApplianceUsagesListDto>(result);
            // Check num sites
            Assert.Equal(3, result.Items.Count);
            // Get the last au
            var auDto = result.Items.Last();
            // Check appliance name
            Assert.Equal("Car", auDto.ApplianceDto.Name);
            // Check quantity
            Assert.Equal(2, auDto.Quantity);
        }

    }
}
