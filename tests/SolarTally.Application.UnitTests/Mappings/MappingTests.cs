using System.Linq;
using AutoMapper;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.Consumptions.Queries.Dtos;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;
using Xunit;

namespace SolarTally.Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapSiteToSiteDto()
        {
            var entity = new Site("A Site");
            entity.MainAddress = new Address("0 Bloor St.", "Toronto",
                "Ontario", "Canada", "M1N2O3");

            var result = _mapper.Map<SiteDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<SiteDto>(result);
            Assert.Equal(entity.MainAddress, result.MainAddress);
        }

        [Fact]
        public void ShouldMapSiteToSiteBriefDto()
        {
            var entity = new Site("A Site");
            entity.MainAddress = new Address("0 Bloor St.", "Toronto",
                "Ontario", "Canada", "M1N2O3");
            
            var result = _mapper.Map<SiteBriefDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<SiteBriefDto>(result);
            Assert.Equal(entity.MainAddress.City, result.MainAddressCity);
        }

        [Fact]
        public void ShouldMapConsumptionToConsumptionDTO()
        {
            var site = new Site("A Site");
            decimal totalPowerConsumption = 500;
            decimal totalStartupPowerConsumption = 700;
            site.Consumption.AddApplianceUsage(
                new Appliance("TV", "Television", totalPowerConsumption,
                    totalStartupPowerConsumption));

            var result = _mapper.Map<ConsumptionDto>(site.Consumption);

            Assert.NotNull(result);
            Assert.IsType<ConsumptionDto>(result);
            Assert.Equal(totalPowerConsumption,
                result.ConsumptionTotal.TotalPowerConsumption);
            Assert.Single(result.ApplianceUsages);
        }

        [Fact]
        public void ShouldMapApplianceUsageToApplianceUsageDto()
        {
            var site = new Site("A Site");
            site.Consumption.AddApplianceUsage(
                new Appliance("TV", "Television", 500, 700));
            var au = site.Consumption.ApplianceUsages.Last();

            var result = _mapper.Map<ApplianceUsageDto>(au);

            Assert.NotNull(result);
            Assert.IsType<ApplianceUsageDto>(result);
            Assert.Equal(au.GetNumHours(), 
                result.ApplianceUsageScheduleDto.Hours);
        }
    }
}