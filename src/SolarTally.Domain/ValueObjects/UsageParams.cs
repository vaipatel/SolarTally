using System.Collections.Generic;
using Ardalis.GuardClauses;
using SolarTally.Domain.Common;

namespace SolarTally.Domain.ValueObjects
{
    /// <summary>
    /// Represents the parameters of usage of any appliance by any consumer.
    /// </summary>
    public class UsageParams : ValueObject
    {
        /// <summary>The number of appliances owned.</summary>
        public int Quantity { get; private set; }

        /// <summary>The amount of power consumed in kW.</summary>
        public decimal PowerConsumption { get; private set; }

        /// <summary>
        /// Hrs to run the appliance.
        /// </summary>
        /// <remark>
        /// TODO: Calculate this from Noda LocalTime or similar to make it
        /// easier for the user to input this info.
        /// </remark>
        public int NumHours { get; private set; }

        /// <summary>
        /// Percent of hrs to run the appliance when solar is available.
        /// </summary>
        public decimal PercentHrsOnSolar { get; private set; }

        private UsageParams()
        {
            // Apparently required for EF
        }

        public UsageParams(int quantity, decimal powerConsumption,
            int numHours, int percentHrsOnSolar)
        {
            Guard.Against.LessThan(quantity, nameof(quantity), 0);
            Guard.Against.LessThan(powerConsumption,
                nameof(powerConsumption), 0);
            Guard.Against.OutOfRange(numHours, 
                nameof(numHours), 0, 24);
            Guard.Against.OutOfRange(percentHrsOnSolar,
                nameof(percentHrsOnSolar), 0, 1);

            Quantity = quantity;
            PowerConsumption = powerConsumption;
            NumHours = numHours;
            PercentHrsOnSolar = percentHrsOnSolar;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Quantity;
            yield return PowerConsumption;
            yield return NumHours;
            yield return PercentHrsOnSolar;
        }
    }
}