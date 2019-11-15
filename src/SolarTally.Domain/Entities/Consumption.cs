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
        // Site Nav prop
        public Site Site { get; private set; }
        public IReadOnlySiteSettings ReadOnlySiteSettings => Site;

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
            Site = site;
            this.Recalculate();
        }

        public void AddApplianceUsage(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            var applianceUsage = new ApplianceUsage(this, appliance,
                ApplianceUsage.DefaultQuantity,
                appliance.DefaultPowerConsumption, Site.NumSolarHours,
                ApplianceUsage.DefaultNumHoursOffSolar, true);
            applianceUsage.HandleSolarIntervalUpdated(addIfEmpty: true);
            _applianceUsages.Add(applianceUsage);
            this.Recalculate();
        }

        public void Recalculate()
        {
            ConsumptionTotal = new ConsumptionTotal(this);
        }
    }
}