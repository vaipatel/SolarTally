using System.Collections.Generic;
using System.Linq;
using SolarTally.Domain.Common;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Encapsulates details of total usage across all appliances
    /// </summary>
    public class Consumption : BaseEntity<int>, IAggregateRoot
    {
        // Site Foreign Key
        public int SiteId { get; private set; }
        // Site Nav prop
        public Site Site { get; private set; }

        private readonly List<ApplianceUsage> _applianceUsages;
        public IReadOnlyCollection<ApplianceUsage> ApplianceUsages =>
            _applianceUsages;

        private Consumption()
        {
            // Apparently required for EF
        }

        public Consumption(Site site)
        {
            _applianceUsages = new List<ApplianceUsage>();
            SiteId = site.Id;
            Site = site;
        }

        public void AddApplianceUsage(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            var applianceUsage = new ApplianceUsage(appliance,
                ApplianceUsage.DefaultQuantity,
                appliance.DefaultPowerConsumption, Site.NumSolarHours,
                ApplianceUsage.DefaultPercentHrsOnSolar, true);
            _applianceUsages.Add(applianceUsage);
        }

        public void UpdateApplianceUsageHours(int applianceUsageId,
            int newNumHours)
        {
            var applianceUsage = _applianceUsages
                .Where(au => au.Id == applianceUsageId)
                .Single();
            Guard.Against.Null(applianceUsage, nameof(applianceUsage));
            Guard.Against.InvalidApplianceUsageHours(newNumHours,
                applianceUsage.PercentHrsOnSolar, Site.NumSolarHours);
            // Below should be fine bcoz I think applianceUsage is a ref ..
            applianceUsage.SetNumHours(newNumHours);
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