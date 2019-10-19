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
        /// Hrs to run the appliance when solar is available.
        /// </summary>
        /// <remark>
        /// TODO: Calculate this from Noda LocalTime or similar to make it
        /// easier for the user to input this info.
        /// </remark>
        public int NumHoursWithSolar { get; private set; }

        /// <summary>
        /// Hrs to run the appliance when solar is unavailable.
        /// </summary>
        /// <remark>
        /// TODO: Calculate this from Noda LocalTime or similar to make it
        /// easier for the user to input this info.
        /// </remark>
        public int NumHoursWithoutSolar { get; private set; }

        private UsageParams()
        {
            // Apparently required for EF
        }

        public UsageParams(int quantity, decimal powerConsumption,
            int numHoursWithSolar, int numHoursWithoutSolar)
        {
            Guard.Against.LessThan(quantity, nameof(quantity), 0);
            Guard.Against.LessThan(powerConsumption,
                nameof(powerConsumption), 0);
            Guard.Against.LessThan(numHoursWithSolar, 
                nameof(numHoursWithSolar), 0);
            Guard.Against.LessThan(numHoursWithoutSolar,
                nameof(numHoursWithoutSolar), 0);

            Quantity = quantity;
            PowerConsumption = powerConsumption;
            NumHoursWithSolar = numHoursWithSolar;
            NumHoursWithoutSolar = NumHoursWithoutSolar;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Quantity;
            yield return PowerConsumption;
            yield return NumHoursWithSolar;
            yield return NumHoursWithoutSolar;
        }
    }
}