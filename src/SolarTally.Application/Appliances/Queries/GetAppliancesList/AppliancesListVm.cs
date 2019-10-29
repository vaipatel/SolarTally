using System.Collections.Generic;
using SolarTally.Application.Appliances.Queries.Dtos;

namespace SolarTally.Application.Appliances.Queries.GetAppliancesList
{
    public class AppliancesListVm
    {
        public IList<ApplianceDto> Appliances { get; set; }
    }
}