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

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplianceUsage, ApplianceUsageDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.ApplianceDto, 
                    opt => opt.MapFrom(s => s.Appliance))
                .ForMember(d => d.Quantity, 
                    opt => opt.MapFrom(s => s.Quantity))
                .ForMember(d => d.PowerConsumption,
                    opt => opt.MapFrom(s => s.PowerConsumption))
                .ForMember(d => d.NumHours,
                    opt => opt.MapFrom(s => s.NumHours))
                .ForMember(d => d.PercentHrsOnSolar,
                    opt => opt.MapFrom(s => s.PercentHrsOnSolar))
                .ForMember(d => d.Enabled,
                    opt => opt.MapFrom(s => s.Enabled));
        }
    }
}