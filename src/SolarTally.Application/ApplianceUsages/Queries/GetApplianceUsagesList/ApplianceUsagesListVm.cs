using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using System.Collections.Generic;

namespace SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesList
{
    public class ApplianceUsagesListVm
    {
        public IList<ApplianceUsageDto> ApplianceUsages { get; set; }
    }
}