using System;
using System.Collections.Generic;
using AutoMapper;
using SolarTally.Application.Common.Mappings;
using SolarTally.Domain.Entities;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Application.ApplianceUsages.Queries.Dtos
{
    public class ApplianceUsageScheduleDto: IMapFrom<ApplianceUsageSchedule>
    {
        public IReadOnlyCollection<UsageTimeInterval> UsageIntervals {get; set;}

        public TimeSpan TotalTimeOnSolar {get; set;}
        public TimeSpan TotalTimeOffSolar {get; set;}

        public decimal HoursOnSolar {get; set;}
        public decimal HoursOffSolar {get; set;}
        public decimal Hours {get; set;}

        public void Mapping(Profile profile)
        {
            profile
                .CreateMap<ApplianceUsageSchedule, ApplianceUsageScheduleDto>()
                // .ForMember(d => d.UsageIntervals, 
                //     opt => opt.MapFrom(s => s.UsageIntervals))
                ;
        }
    }
}