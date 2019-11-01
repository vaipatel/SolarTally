using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Appliances.Queries.Dtos
{
    public class ApplianceDto : IMapFrom<Appliance>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DefaultPowerConsumption { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Appliance, ApplianceDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Description, 
                    opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.DefaultPowerConsumption,
                    opt => opt.MapFrom(s => s.DefaultPowerConsumption));
        }
    }
}