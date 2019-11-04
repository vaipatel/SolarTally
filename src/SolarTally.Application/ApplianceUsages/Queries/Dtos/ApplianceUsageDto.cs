using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;
using SolarTally.Application.Appliances.Queries.Dtos;

namespace SolarTally.Application.ApplianceUsages.Queries.Dtos
{
    public class ApplianceUsageDto : IMapFrom<ApplianceUsage>
    {
        public int Id { get; set; }

        public ApplianceDto ApplianceDto { get; set; }

        public int Quantity { get; set; }

        public decimal PowerConsumption { get; set; }

        public int NumHours { get; set; }

        public decimal PercentHrsOnSolar { get; set; }

        public bool Enabled { get; set; }

        public int ConsumptionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplianceUsage, ApplianceUsageDto>()
                .ForMember(d => d.ApplianceDto, 
                    opt => opt.MapFrom(s => s.Appliance))
                ;
        }
    }
}