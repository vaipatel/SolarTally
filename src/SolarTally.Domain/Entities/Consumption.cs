using System.Collections.Generic;
using System.Linq;
using SolarTally.Domain.Common;
using SolarTally.Domain.Interfaces;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    public class Consumption : BaseEntity<int>, IAggregateRoot
    {
        // Site Foreign Key
        public int SiteId { get; set; }
        // Site Nav prop
        public Site Site { get; set; }

        private readonly List<ApplianceUsage> _applianceUsages;
        public IReadOnlyCollection<ApplianceUsage> ApplianceUsages =>
            _applianceUsages;

        public Consumption()
        {
            _applianceUsages = new List<ApplianceUsage>();
        }

        public void ModifyAppliance(int applianceUsageId, Appliance appliance)
        {
            var applianceUsage = _applianceUsages
                .Where(au => au.Id == applianceUsageId)
                .Single();
            Guard.Against.Null(applianceUsage, nameof(applianceUsage));
            if (applianceUsage.ApplianceId != appliance.Id)
            {
                applianceUsage.SetAppliance(appliance);
            }
        }
    }
}