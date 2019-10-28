using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.Sites.Queries.GetSitesList
{
    public class SiteDto : IMapFrom<Site>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumSolarHours { get; set; }

        public Address MainAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Site, SiteDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.NumSolarHours, 
                    opt => opt.MapFrom(s => s.NumSolarHours))
                .ForMember(d => d.MainAddress,
                    opt => opt.MapFrom(s => s.MainAddress));
        }
    }
}