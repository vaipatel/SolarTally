using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Application.Consumptions.Queries.Dtos;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SiteDto : IMapFrom<Site>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumSolarHours { get; set; }

        public Address MainAddress { get; set; }

        public ConsumptionDto Consumption { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Site, SiteDto>()
                ;
        }
    }
}