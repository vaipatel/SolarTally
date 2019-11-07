using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Application.Consumptions.Queries.Dtos;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SiteBriefDto : IMapFrom<Site>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MainAddressCity { get; set; }

        public ConsumptionTotal ConsumptionTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Site, SiteBriefDto>()
                .ForMember(d => d.ConsumptionTotal,
                    o => o.Ignore())
                ;
        }
    }
}