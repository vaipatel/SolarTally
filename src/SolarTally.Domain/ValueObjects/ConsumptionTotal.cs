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

        private decimal GetMaxPowerConsumption(Consumption consumption)
        {
            var mergedUTIs = new List<UsageTimeInterval>();
            var mergedPowers = new List<decimal>();

            var numAUs = consumption.ApplianceUsages.Count;
            for (int auIdx = 0; auIdx < numAUs; ++auIdx)
            {
                var au = consumption.ApplianceUsages.ElementAt(auIdx);
                var auPower = au.ApplianceUsageTotal.TotalPowerConsumption;
                // TODO: Also get the startup consumption
                var utisForAU = au.ApplianceUsageSchedule.UsageIntervals;
                var numUtisForAU = utisForAU.Count;
                for (int utiIdx = 0; utiIdx < numUtisForAU; ++utiIdx)
                {
                    var uti = utisForAU.ElementAt(utiIdx);
                    if (uti.UsageKind != UsageKind.UsingSolar) continue;
                    CombineSolarIntervals(mergedUTIs, mergedPowers, uti,
                        auPower);
                }
            }

            var maxPower = (mergedPowers.Count == 0) ? 0 : mergedPowers.Max();
            return maxPower;
        }

        private static void CombineSolarIntervals(
            List<UsageTimeInterval> A, 
            List<decimal> APowers,
            UsageTimeInterval b, decimal bPower, int startIdx = 0)
        {
            if (b.TimeInterval.Start == b.TimeInterval.End) return;
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
            for (int i = startIdx; i < A.Count; ++i)
            {
                var uti_curr = A[i];
                var uti_curr_start = uti_curr.TimeInterval.Start;
                var uti_curr_end = uti_curr.TimeInterval.End;
                var uti_curr_diff = uti_curr.TimeInterval.Difference;
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
                            if (b_end < uti_prev_end)
                            {
                                A.Insert(i, b);
                                APowers.Insert(i, APowers_prev + bPower); //startup
                                var uAfter = new UsageTimeInterval(
                                    new TimeInterval(b_end.Hours, b_end.Minutes,
                                        uti_prev_end.Hours,
                                        uti_prev_end.Minutes));
                                A.Insert(i+1, uAfter);
                                APowers.Insert(i+1, APowers_prev);
                                return;
                            }
                            else
                            {
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
                                    b_trimmed, bPower, i);
                                return;
                            }
                        }
                        // else (b_start >= uti_prev_end)
                        {
                            APowers.Insert(i, bPower); //startup
                            if (b_end <= uti_curr_start)
                            {
                                A.Insert(i, b);
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
                                var b_trimmed = new UsageTimeInterval(
                                    new TimeInterval(
                                        uti_curr_start.Hours,
                                        uti_curr_start.Minutes,
                                        b_end.Hours, b_end.Minutes));
                                CombineSolarIntervals(A, APowers,
                                    b_trimmed, bPower, i);
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
                            b_trimmed, bPower, i);
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
                    else
                    {
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
    }
}