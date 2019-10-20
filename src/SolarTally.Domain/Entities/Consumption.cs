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
            var applianceUsage = new ApplianceUsage(appliance, 1,
                appliance.DefaultPowerConsumption, Site.NumSolarHours, 1, true);
            _applianceUsages.Add(applianceUsage);
        }

        public void AddApplianceUsageWithId(int id, Appliance appliance)
        {
            Guard.Against.AlreadyHasOne(_applianceUsages,
                nameof(_applianceUsages), au => au.Id == id);
            AddApplianceUsage(appliance);
            _applianceUsages.Last().Id = id;
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