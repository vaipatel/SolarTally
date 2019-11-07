using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using System.Collections.Generic;

namespace SolarTally.Application.ApplianceUsages.Queries.Dtos
{
    public class ApplianceUsagesListDto
    {
        public IList<ApplianceUsageDto> ApplianceUsages { get; set; }
    }
}