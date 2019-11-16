using System.Collections.Generic;
using System.Linq;
using SolarTally.Domain.Common;
using SolarTally.Domain.Entities;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.ValueObjects
{
    /// <summary>
    /// Encapsulates the calculated totals for a single ApplianceUsage record,
    /// given the quantity, power consumption in watts and hrs of operations.
    /// </summary>
    public class ConsumptionTotal : ValueObject
    {
        public decimal TotalPowerConsumption { get; private set; }
        public decimal TotalOnSolarEnergyConsumption { get; private set; }
        public decimal TotalOffSolarEnergyConsumption { get; private set; }
        public decimal TotalEnergyConsumption { get; private set; }

        private ConsumptionTotal() { }

        public ConsumptionTotal(Consumption consumption)
        {
            TotalPowerConsumption = 0;
            TotalOnSolarEnergyConsumption = 0;
            TotalOffSolarEnergyConsumption = 0;
            foreach(var au in consumption.ApplianceUsages)
            {
                TotalPowerConsumption += 
                    au.ApplianceUsageTotal.TotalPowerConsumption;
                TotalOnSolarEnergyConsumption +=
                    au.ApplianceUsageTotal.TotalOnSolarEnergyConsumption;
                TotalOffSolarEnergyConsumption +=
                    au.ApplianceUsageTotal.TotalOffSolarEnergyConsumption;
            }
            TotalEnergyConsumption = TotalOnSolarEnergyConsumption +
                TotalOffSolarEnergyConsumption;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return TotalPowerConsumption;
            yield return TotalOnSolarEnergyConsumption;
            yield return TotalOffSolarEnergyConsumption;
            yield return TotalEnergyConsumption;
        }

        private decimal GetMaxPowerConsumption(Consumption consumption)
        {
            // First get the total number of usage intervals
            var idxes = new List<int>();
            var counts = new List<int>();
            var totalSteps = 0;
            foreach(var au in consumption.ApplianceUsages)
            {
                var count = au.ApplianceUsageSchedule.UsageIntervals.Count;
                totalSteps += count;
                counts.Add(count);
                idxes.Add(0);
            }

            var numAUs = consumption.ApplianceUsages.Count;
            decimal currMaxPower = 0;
            // While we haven't finished stepping thru all AU schedules
            while (!idxes.SequenceEqual(counts))
            {
                // 1. Compare UsageTimeIntervals at idxes to get earliest
                UsageTimeInterval currEarliest = null;
                decimal currPowerSum = 0;
                int lastAdvancedIdx = -1;
                for (int auIdx = 0; auIdx < numAUs; ++auIdx)
                {
                    var idx = idxes[auIdx];
                    
                    // If idxes[auIdx] == counts[auIdx], this AU is over
                    if (idx == counts[auIdx])
                    {
                        continue;
                    }

                    var auAtIdx = consumption.ApplianceUsages.ElementAt(auIdx); // Note: ElementAt inefficient
                    var utiAtIdx = auAtIdx
                        .ApplianceUsageSchedule.UsageIntervals.ElementAt(idx); // Note: ElementAt inefficient
                    
                    // We only care about Max Power Consumption for our
                    // solar inverters.
                    if (utiAtIdx.UsageKind != UsageKind.UsingSolar)
                    {
                        // 2. Advance 
                        ++idxes[auIdx];
                        lastAdvancedIdx = auIdx;
                        continue;
                    }

                    // If earliest has not been set yet,
                    if (currEarliest == null)
                    {
                        // Make this the currEarliest and compare back from next
                        currEarliest = utiAtIdx;
                        currPowerSum = auAtIdx.PowerConsumption; // TODO: Make it StartupPowerConsumption
                        // 2. Advance 
                        ++idxes[auIdx];
                        lastAdvancedIdx = auIdx;
                        continue;
                    }
                    else // Compare to prev
                    {
                        if (utiAtIdx.TimeInterval.Start < 
                            currEarliest.TimeInterval.Start)
                        {
                            currEarliest = utiAtIdx;
                            currPowerSum = auAtIdx.PowerConsumption;
                            // 2. Advance 
                            ++idxes[auIdx];
                            // 2.1. Retreat last advanced idx because its UTI
                            // happens later compared to the UTI at this idx
                            --idxes[lastAdvancedIdx];
                            lastAdvancedIdx = auIdx;
                        }
                        else if (utiAtIdx.TimeInterval.Start == 
                            currEarliest.TimeInterval.Start)
                        {
                            currPowerSum += auAtIdx.PowerConsumption; // TODO: Make it StartupPowerConsumption
                            // 2. Advance
                            ++idxes[auIdx];
                            lastAdvancedIdx = auIdx;
                        }
                        else
                        {
                            var prevIdx = idx - 1;
                            var utiAtPrevIdx = auAtIdx
                                .ApplianceUsageSchedule.UsageIntervals
                                .ElementAt(prevIdx); // Note: ElementAt inefficient
                            
                        }
                    }
                }
                currMaxPower = System.Math.Max(currMaxPower, currPowerSum);
            }
            return currMaxPower;
        }
    }
}