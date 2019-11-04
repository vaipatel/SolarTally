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

        public IReadOnlyCollection<ApplianceUsageDto> ApplianceUsages
        { get; set; }

        public ConsumptionTotal ConsumptionTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Consumption, ConsumptionDto>()
                ;
        }
    }
}