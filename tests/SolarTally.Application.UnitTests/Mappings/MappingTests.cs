using AutoMapper;
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
            var entity = new Site("A Site", 9);
            entity.MainAddress = new Address("0 Bloor St.", "Toronto",
                "Ontario", "Canada", "M1N2O3");

            var result = _mapper.Map<SiteDto>(entity);

            Assert.NotNull(result);
            Assert.IsType<SiteDto>(result);
            Assert.Equal(entity.MainAddress, result.MainAddress);
        }
    }
}