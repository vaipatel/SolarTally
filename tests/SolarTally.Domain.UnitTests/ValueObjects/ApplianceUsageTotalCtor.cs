using System.Collections.Generic;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.Domain.UnitTests.Builders;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class ApplianceUsageTotalCtor
    {
        [Theory]
        [ClassData(typeof(AUTestData))]
        public void ShouldCalcTheCorrectTotals(
            int quantity,
            decimal powerConsumption,
            AUSchedule aUSchedule,
            decimal expTotalPowerConsumption,
            decimal expTotalOnSolarEnergyConsumption,
            decimal expTotalOffSolarEnergyConsumption,
            decimal expTotalEnergyConsumption)
        {
            var builder = new ApplianceUsageBuilder();
            var applianceUsage = new ApplianceUsage(
                builder.TestConsumptionCalculator, builder.TestAppliance,
                quantity, powerConsumption, builder.TestEnabled);
            applianceUsage.ApplianceUsageSchedule.ClearUsageIntervals();
            foreach(var u in aUSchedule.UsageIntervals)
            {
                var ti = u.TimeInterval;
                int startHr = ti.Start.Hours, startMin = ti.Start.Minutes;
                int endHr   = ti.End.Hours,   endMin   = ti.End.Minutes;
                applianceUsage.ApplianceUsageSchedule.AddUsageInterval(
                    startHr, startMin, endHr, endMin, u.UsageKind
                );
            }
            var applianceUsageTotal = new ApplianceUsageTotal(applianceUsage);

            Assert.Equal(expTotalPowerConsumption,
                applianceUsageTotal.TotalPowerConsumption);
            Assert.Equal(expTotalOnSolarEnergyConsumption,
                applianceUsageTotal.TotalOnSolarEnergyConsumption);
            Assert.Equal(expTotalOffSolarEnergyConsumption,
                applianceUsageTotal.TotalOffSolarEnergyConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                applianceUsageTotal.TotalEnergyConsumption);
        }

        public class AUTestDataItem
        {
            public int Quantity { get; set; }
            public decimal PowerConsumption { get; set; }
            public AUSchedule AUSchedule { get; set; }
            public decimal ExpTotalPowerConsumption { get; set; }
            public decimal ExpTotalOnSolarEnergyConsumption { get; set; }
            public decimal ExpTotalOffSolarEnergyConsumption { get; set; }
            public decimal ExpTotalEnergyConsumption { get; set; }
        }

        public static IEnumerable<AUTestDataItem> TestData =>
            new List<AUTestDataItem>
            {
                // All zeros
                new AUTestDataItem {
                    Quantity = 0,
                    PowerConsumption = 0,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,8,0
                            ), UsageKind.UsingSolar)
                        }
                    },
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero Quantity
                new AUTestDataItem {
                    Quantity = 0,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,15,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,21,0
                            ), UsageKind.UsingBattery),
                        }
                    },
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero PowerConsumption
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 0,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,15,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,21,0
                            ), UsageKind.UsingBattery),
                        }
                    },
                    ExpTotalPowerConsumption = 0,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 0
                },
                // Zero NumHoursOnSolar
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,8,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,21,0
                            ), UsageKind.UsingBattery),
                        }
                    },
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 0,
                    ExpTotalOffSolarEnergyConsumption = 60,
                    ExpTotalEnergyConsumption = 60
                },
                // Zero NumHoursOffSolar
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,11,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,18,0
                            ), UsageKind.UsingBattery),
                        }
                    },
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 60,
                    ExpTotalOffSolarEnergyConsumption = 0,
                    ExpTotalEnergyConsumption = 60
                },
                // Quantity=NumOnSolarHours=NumOffSolarHours=1
                new AUTestDataItem {
                    Quantity = 1,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,9,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,19,0
                            ), UsageKind.UsingBattery),
                        }
                    },
                    ExpTotalPowerConsumption = 20,
                    ExpTotalOnSolarEnergyConsumption = 20,
                    ExpTotalOffSolarEnergyConsumption = 20,
                    ExpTotalEnergyConsumption = 40
                },
                // Quantity=NumOnSolarHours=NumOffSolarHours=2
                new AUTestDataItem {
                    Quantity = 2,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,9,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                11,30,12,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                13,30,14,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,19,0
                            ), UsageKind.UsingBattery),
                            new UsageTimeInterval(new TimeInterval(
                                19,0,20,0
                            ), UsageKind.UsingMains),
                        }
                    },
                    ExpTotalPowerConsumption = 40,
                    ExpTotalOnSolarEnergyConsumption = 80,
                    ExpTotalOffSolarEnergyConsumption = 80,
                    ExpTotalEnergyConsumption = 160
                },
                // Mutually unequal Quantity,NumOnSolarHours,NumOffSolarHours
                new AUTestDataItem {
                    Quantity = 2,
                    PowerConsumption = 20,
                    AUSchedule = new AUSchedule {
                        UsageIntervals = new List<UsageTimeInterval> {
                            new UsageTimeInterval(new TimeInterval(
                                8,0,10,0
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                11,30,12,30
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                13,30,14,30
                            ), UsageKind.UsingSolar),
                            new UsageTimeInterval(new TimeInterval(
                                18,0,19,0
                            ), UsageKind.UsingBattery),
                            new UsageTimeInterval(new TimeInterval(
                                19,0,21,0
                            ), UsageKind.UsingMains),
                        }
                    },
                    ExpTotalPowerConsumption = 2 * 20,
                    ExpTotalOnSolarEnergyConsumption = 2 * 20 * 4,
                    ExpTotalOffSolarEnergyConsumption = 2 * 20 * 3,
                    ExpTotalEnergyConsumption = 2 * 20 * (4 + 3)
                }
            };
        
        public class AUTestData : TheoryData<int, decimal, AUSchedule, 
            decimal, decimal, decimal, decimal>
        {
            public AUTestData()
            {
                foreach(var au in TestData)
                {
                    Add(
                        au.Quantity,
                        au.PowerConsumption,
                        au.AUSchedule,
                        au.ExpTotalPowerConsumption,
                        au.ExpTotalOnSolarEnergyConsumption,
                        au.ExpTotalOffSolarEnergyConsumption,
                        au.ExpTotalEnergyConsumption
                    );
                }
            }
        }

        public class AUSchedule
        {
            public List<UsageTimeInterval> UsageIntervals { get; set; }
        }

    }
}