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

        public decimal DefaultStartupPowerConsumption { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Appliance, ApplianceDto>();
        }
    }
}