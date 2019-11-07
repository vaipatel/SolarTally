using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using System.Collections.Generic;

namespace SolarTally.Application.ApplianceUsages.Queries.Dtos
{
    public class ApplianceUsagesListVm
    {
        public IList<ApplianceUsageDto> ApplianceUsageDtos { get; set; }
    }
}