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
    public class Consumption : BaseEntity<int>, IAggregateRoot,
        IConsumptionCalculator
    {
        // Site Foreign Key
        public int SiteId { get; private set; }
        // Site Nav prop
        public Site Site { get; private set; }

        private readonly List<ApplianceUsage> _applianceUsages;
        public IReadOnlyCollection<ApplianceUsage> ApplianceUsages =>
            _applianceUsages;

        public ConsumptionTotal ConsumptionTotal { get; private set; }

        private Consumption()
        {
            // Apparently required for EF
        }

        public Consumption(Site site)
        {
            _applianceUsages = new List<ApplianceUsage>();
            SiteId = site.Id;
            Site = site;
            this.Recalculate();
        }

        public void AddApplianceUsage(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            var applianceUsage = new ApplianceUsage(this, appliance,
                ApplianceUsage.DefaultQuantity,
                appliance.DefaultPowerConsumption, Site.NumSolarHours,
                ApplianceUsage.DefaultPercentHrsOnSolar, true);
            _applianceUsages.Add(applianceUsage);
            this.Recalculate();
        }

        public int GetSiteNumSolarHours()
        {
            return Site.NumSolarHours;
        }

        public void Recalculate()
        {
            ConsumptionTotal = new ConsumptionTotal(this);
        }
    }
}