using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Application.Consumptions.Queries.Dtos;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Sites.Queries.Dtos
{
    public class SiteDetailVm : IMapFrom<Site>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumSolarHours { get; set; }

        public Address MainAddress { get; set; }

        public ConsumptionTotal ConsumptionTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Site, SiteDetailVm>()
                .ForMember(d => d.ConsumptionTotal,
                    o => o.Ignore())
                ;
        }
    }
}