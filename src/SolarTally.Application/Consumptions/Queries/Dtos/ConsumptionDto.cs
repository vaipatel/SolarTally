using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Consumptions.Queries.Dtos
{
    public class ConsumptionDto : IMapFrom<Consumption>
    {
        public int Id { get; set; }

        public SiteDto Site { get; set; }

        // public IReadOnlyCollection<ApplianceUsageDto> ApplianceUsages
        // { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consumption, ConsumptionDto>()
                // .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                // .ForMember(d => d.Site, opt => opt.MapFrom(s => s.Site))
                // .ForMember(d => d.ApplianceUsages,
                //     opt => opt.MapFrom(s => s.ApplianceUsages)
                // )
                ;
        }

        // public IReadOnlyCollection<ApplianceUsageDto> ConvertAUs(Consumption c)
        // {
        //     var result = new List<ApplianceUsageDto>();
        //     foreach(var au in c.ApplianceUsages)
        //     {
        //         //
        //     }
        //     return new List<ApplianceUsageDto>();
        // }
    }
}