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
        public decimal MaxPowerConsumption { get; private set; }
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
            MaxPowerConsumption = GetMaxPowerConsumption(consumption);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return MaxPowerConsumption;
            yield return TotalPowerConsumption;
            yield return TotalOnSolarEnergyConsumption;
            yield return TotalOffSolarEnergyConsumption;
            yield return TotalEnergyConsumption;
        }

        public static void CombineSolarIntervals(
            List<UsageTimeInterval> A, 
            List<decimal> APowers,
            UsageTimeInterval b, decimal bPower)
        {
            if (A.Count == 0)
            {
                A.Add(b);
                APowers.Add(bPower);
                return;
            }
            // Assume A.length == APowers.length
            // Assume all UsageTimeIntervals in A, and b, are UsingSolars
            var b_start = b.TimeInterval.Start;
            var b_end = b.TimeInterval.End;
            var b_diff = b.TimeInterval.Difference;
            for (int i = 0; i < A.Count; ++i)
            {
                var uti_curr = A[i];
                var uti_curr_start = uti_curr.TimeInterval.Start;
                var uti_curr_end = uti_curr.TimeInterval.End;
                var APowers_curr = APowers[i];
                if (b_start < uti_curr_start)
                {
                    if (i > 0)
                    {
                        var uti_prev = A[i-1];
                        var uti_prev_start = uti_prev.TimeInterval.Start;
                        var uti_prev_end = uti_prev.TimeInterval.End;
                        var APowers_prev = APowers[i-1];
                        if (b_start < uti_prev_end)
                        {
                            var uBefore = new UsageTimeInterval(
                                new TimeInterval(
                                    uti_prev_start.Hours,uti_prev_start.Minutes,
                                    b_start.Hours, b_start.Minutes)
                            );
                            A.Insert(i-1, uBefore);
                            A.RemoveAt(i);
                            var uAfter = new UsageTimeInterval(
                                new TimeInterval(b_start.Hours, b_start.Minutes,
                                    uti_prev_end.Hours, uti_prev_end.Minutes)
                            );
                            A.Insert(i, uAfter);
                            APowers.Insert(i, APowers_prev + bPower); //startup
                            var b_trimmed = new UsageTimeInterval(
                                new TimeInterval(
                                    uti_prev_end.Hours, uti_prev_end.Minutes,
                                    b_end.Hours, b_end.Minutes));
                            CombineSolarIntervals(A, APowers,
                                b_trimmed, bPower);
                            return;
                        }
                        // else (b_start >= uti_prev_end)
                        {
                            if (b_end <= uti_curr_start)
                            {
                                A.Insert(i, b);
                                APowers.Insert(i, bPower); //startup
                                return;
                            }
                            // else (b_end > uti_curr_start)
                            {
                                var uBefore = new UsageTimeInterval(
                                    new TimeInterval(
                                        b_start.Hours, b_start.Minutes,
                                        uti_curr_start.Hours,
                                        uti_curr_start.Minutes));
                                A.Insert(i, uBefore);
                                APowers.Insert(i, bPower); //startup
                                var b_trimmed = new UsageTimeInterval(
                                    new TimeInterval(
                                        uti_curr_start.Hours,
                                        uti_curr_start.Minutes,
                                        b_end.Hours, b_end.Minutes));
                                CombineSolarIntervals(A, APowers,
                                    b_trimmed, bPower);
                                    return;
                            }
                        }
                    }
                    else // i==0
                    {
                        APowers.Insert(0, bPower); //startup
                        if (b_end <= uti_curr_start)
                        {
                            A.Insert(0, b);
                            return;
                        }
                        var uBefore = new UsageTimeInterval(
                            new TimeInterval(b_start.Hours, b_start.Minutes,
                                uti_curr_start.Hours, uti_curr_start.Minutes));
                        A.Insert(0, uBefore);
                        var b_trimmed = new UsageTimeInterval(
                            new TimeInterval(
                                uti_curr_start.Hours, uti_curr_start.Minutes,
                                b_end.Hours, b_end.Minutes));
                        CombineSolarIntervals(A, APowers,
                            b_trimmed, bPower);
                        return;
                    }
                }
                else if (b_start == uti_curr_start)
                {
                    APowers[i] += bPower; //startup
                    if (b_end > uti_curr_end)
                    {
                        var b_trimmed = new UsageTimeInterval(
                            new TimeInterval(
                                uti_curr_end.Hours, uti_curr_end.Minutes,
                                b_end.Hours, b_end.Minutes));
                        CombineSolarIntervals(A, APowers,
                            b_trimmed, bPower);
                        return;
                    }
                    else if (b_end < uti_curr_end)
                    {
                        A.Insert(i, b);
                        A.RemoveAt(i+1);
                        A.Insert(i+1, new UsageTimeInterval(
                            new TimeInterval(b_end.Hours, b_end.Minutes,
                                uti_curr_end.Hours, uti_curr_end.Minutes)
                        ));
                        APowers.Insert(i+1, APowers_curr);
                        return;
                    }
                }
            } //end for

            var lenA = A.Count;
            var uti_last = A.Last();
            var uti_last_start = uti_last.TimeInterval.Start;
            var uti_last_end = uti_last.TimeInterval.End;
            var APowers_last = APowers[lenA-1];
            if (b_start < uti_last_end)
            {
                var uBefore = new UsageTimeInterval(
                    new TimeInterval(
                        uti_last_start.Hours,uti_last_start.Minutes,
                        b_start.Hours, b_start.Minutes)
                );
                A.Insert(lenA-1, uBefore);
                A.RemoveAt(lenA);
                if (b_end < uti_last_end)
                {
                    A.Add(b);
                    APowers.Add(APowers_last + bPower); //startup
                    var uAfter = new UsageTimeInterval(
                        new TimeInterval(b_end.Hours, b_end.Minutes,
                            uti_last_end.Hours, uti_last_end.Minutes));
                    A.Add(uAfter);
                    APowers.Add(APowers_last);
                    return;
                }
                else
                {
                    var uAfter = new UsageTimeInterval(
                        new TimeInterval(b_start.Hours, b_start.Minutes,
                            uti_last_end.Hours, uti_last_end.Minutes)
                    );
                    A.Add(uAfter);
                    APowers.Add(APowers_last + bPower); //startup
                    var b_trimmed = new UsageTimeInterval(
                        new TimeInterval(
                            uti_last_end.Hours, uti_last_end.Minutes,
                            b_end.Hours, b_end.Minutes));
                    A.Add(b_trimmed);
                    APowers.Add(bPower);
                    return;
                }
            }
            else
            {
                A.Add(b);
                APowers.Add(bPower);
            }
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
                var advancedAUIdxes = new List<int>();
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
                        advancedAUIdxes.Add(auIdx);
                        continue;
                    }

                    // If earliest has not been set yet,
                    if (currEarliest == null)
                    {
                        // Make this the currEarliest and compare back from next
                        currEarliest = utiAtIdx;
                        currPowerSum = 
                            auAtIdx.ApplianceUsageTotal
                            .TotalPowerConsumption; // TODO: Make it StartupPowerConsumption
                        // 2. Advance 
                        ++idxes[auIdx];
                        advancedAUIdxes.Add(auIdx);
                        continue;
                    }
                    else // Compare to prev
                    {
                        // If this uti comes before the currEarliest
                        if (utiAtIdx.TimeInterval.Start < 
                            currEarliest.TimeInterval.Start)
                        {
                            // then forget everything and respect this one
                            currEarliest = utiAtIdx;
                            currPowerSum = 
                                auAtIdx.ApplianceUsageTotal
                                .TotalPowerConsumption; // TODO: Make it StartupPowerConsumption
                            // 2. Advance 
                            ++idxes[auIdx];
                            // 2.1. Retreat all advancedAUIdxes cuz their 
                            // uti comes later
                            foreach(var advancedAUIdx in advancedAUIdxes)
                            {
                                --idxes[advancedAUIdx];
                            }
                            advancedAUIdxes.Clear();
                            advancedAUIdxes.Add(auIdx);
                        }
                        // Else if this uti is at the same as the last
                        else if (utiAtIdx.TimeInterval.Start == 
                            currEarliest.TimeInterval.Start)
                        {
                            // Do a union
                            currPowerSum += 
                                auAtIdx.ApplianceUsageTotal
                                .TotalPowerConsumption; // TODO: Make it StartupPowerConsumption
                            // 2. Advance
                            ++idxes[auIdx];
                            advancedAUIdxes.Add(auIdx);
                        }
                        // Else if this uti comes later but isn't the first for
                        // this AU schedule
                        else if (idx > 0)
                        {
                            // Go back to the prev UTI..
                            var prevIdx = idx - 1;
                            var utiAtPrevIdx = auAtIdx
                                .ApplianceUsageSchedule.UsageIntervals
                                .ElementAt(prevIdx); // Note: ElementAt inefficient
                            // and if it's solar
                            if (utiAtPrevIdx.UsageKind != UsageKind.UsingSolar)
                            {
                                continue;
                            }
                            // and if its interval covers the current uti
                            if (currEarliest.TimeInterval.Start < utiAtPrevIdx.TimeInterval.End)
                            {
                                currPowerSum += 
                                    auAtIdx.ApplianceUsageTotal
                                    .TotalPowerConsumption; // this is correct
                            }
                        }
                    }
                }
                currMaxPower = System.Math.Max(currMaxPower, currPowerSum);
            }
            return currMaxPower;
        }
    }
}