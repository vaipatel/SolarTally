using SolarTally.Domain.Common;
using SolarTally.Domain.Enumerations;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Interfaces;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    /// <summary>
    /// Encapsulates the particular usage of an Appliance for a given Consumer.
    /// </summary>
    /// <remarks> 
    /// I can't decide if ApplianceUsage has an identity in the domain outside 
    /// of Consumption. It sure seems like it doesn't. It seems much more like 
    /// an Address than an Order.
    /// 
    /// Anyway, maybe it's being unique to a customer/user is not reason enough 
    /// to make it a ValueObject. For example, an Order is also mostly unique to
    /// a customer/user.
    /// 
    /// Furthermore, I gotta think - should ApplianceUsage be immutable? Doesn't
    /// seem right to me.
    /// 
    /// I'm making it an Entity for now. We shall see.
    /// </remarks>
    public class ApplianceUsage : IApplianceUsage, IApplianceUsageInfo
    {
        // Some constants to reduce magic numbers
        public const int DefaultQuantity = 1;

        public int ApplianceId { get; private set; }
        public Appliance Appliance { get; private set; }

        public int ApplianceUsageScheduleId { get; private set; }
        public ApplianceUsageSchedule ApplianceUsageSchedule
        { get; private set; }

        private IConsumptionCalculator _consumptionCalculator { get; set; }

        /// <summary>
        /// FK back to the Consumption that owns this ApplianceUsage
        /// </summary>
        /// <remarks>
        /// I need a FK back to Consumption for Cascade Delete to work without
        /// having a nav prop, I think.
        /// See <a href="https://docs.microsoft.com/en-us/ef/core/modeling/relationships#without-navigation-property">docs on efcore</a>.
        ///
        /// Hopefully this won't "leak" out too much - not much damage can be
        /// done without having all the Site entities, at which point you also
        /// own all the Consumption entities.
        /// Additionally, the Site cannot be gotten from the ConsumptionId or
        /// this ApplianceUsage.
        /// </remarks>
        public int ConsumptionId { get; set; }

        /// <summary>The number of appliances owned.</summary>
        public int Quantity { get; private set; }
        public int GetQuantity() => Quantity;

        /// <summary>The amount of power consumed in Watts.</summary>
        public decimal PowerConsumption { get; private set; }
        public decimal GetPowerConsumption() => PowerConsumption;

        public decimal GetNumHours() => ApplianceUsageSchedule.Hours;

        public decimal GetNumHoursOnSolar() =>
            ApplianceUsageSchedule.HoursOnSolar;
        
        public decimal GetNumHoursOffSolar() =>
            ApplianceUsageSchedule.HoursOffSolar;

        /// <summary>
        /// Whether this ApplianceUsage should be considered in the Consumption.
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Calculated Usage Totals for this ApplianceUsage.
        /// </summary>
        public ApplianceUsageTotal ApplianceUsageTotal { get; private set; }

        private ApplianceUsage()
        {
            // Apparently required by EF Core
        }

        /// <summary>
        /// Makes an ApplianceUsage line.
        /// </summary>
        /// <remarks>
        /// We're passing in an IConsumptionCalculator so that we can get
        /// the Consumption/Site info like NumSolarHours and tell the
        /// Consumption it should Recalculate(), but without having to
        /// pass in the entire Consumption object, and thus avoiding any
        /// unintended consequences through, say, some weird list-manipulation.
        /// </remarks>
        public ApplianceUsage(IConsumptionCalculator consumptionCalculator,
            Appliance appliance, int quantity, decimal powerConsumption,
            bool enabled)
        {
            _consumptionCalculator = consumptionCalculator;
            ApplianceUsageSchedule = 
                new ApplianceUsageSchedule(
                    consumptionCalculator.ReadOnlySiteSettings);
            this.AddPeakSolarIntervalToSchedule();
            this.SetAppliance(appliance);
            this.SetQuantity(quantity);
            this.SetPowerConsumption(powerConsumption);
            this.SetEnabled(enabled);
            // Recalculate
            this.Recalculate();
        }

        public void SetAppliance(Appliance appliance)
        {
            Guard.Against.Null(appliance, nameof(appliance));
            ApplianceId = appliance.Id;
            Appliance = appliance;
        }

        protected override void _SetQuantity(int quantity)
        {
            Guard.Against.LessThan(quantity, nameof(quantity), 0);
            Quantity = quantity;
        }

        protected override void _SetPowerConsumption(decimal powerConsumption)
        {
            Guard.Against.LessThan(powerConsumption,
                nameof(powerConsumption), 0);
            PowerConsumption = powerConsumption;
        }

        protected override void _SetPowerConsumptionToDefault()
        {
            this.SetPowerConsumption(Appliance.DefaultPowerConsumption);
        }

        protected override void _SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        protected override void _HandleSolarIntervalUpdated()
        {
           ApplianceUsageSchedule.HandlePeakSolarIntervalUpdated();
        }

        /// <summary>
        /// Recalcs the ApplianceUsageTotal for this and then asks Consumption
        /// to recalc overall totals.
        /// </summary>
        public override void Recalculate()
        {
            ApplianceUsageTotal = new ApplianceUsageTotal(this);
            _consumptionCalculator.Recalculate();
        }

        private void AddPeakSolarIntervalToSchedule()
        {
            var ti = _consumptionCalculator
                .ReadOnlySiteSettings
                .PeakSolarInterval;
            int peakStartHr = ti.Start.Hours, peakStartMin = ti.Start.Minutes;
            int peakEndHr   = ti.End.Hours,   peakEndMin   = ti.End.Minutes;
            
            ApplianceUsageSchedule.AddUsageInterval(
                peakStartHr, peakStartMin, peakEndHr, peakEndMin,
                UsageKind.UsingSolar);
        }
    }
}