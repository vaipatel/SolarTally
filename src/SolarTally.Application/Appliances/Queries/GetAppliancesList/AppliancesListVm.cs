using System.Collections.Generic;

namespace SolarTally.Application.Appliances.Queries.GetAppliancesList
{
    public class AppliancesListVm
    {
        public IList<ApplianceDto> Appliances { get; set; }
    }
}