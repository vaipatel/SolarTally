using System.Collections.Generic;
using System.Linq;
using Xunit;
using SolarTally.Domain.ValueObjects;
using SolarTally.Domain.Entities;
using SolarTally.Domain.UnitTests.Builders;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.UnitTests.ValueObjects
{
    public class ConsumptionTotalCtor
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldCalcTheCorrectTotals(
            List<CompactApplianceUsage> inApplianceUsages,
            decimal expMaxPowerConsumption,
            decimal expTotalPowerConsumption,
            decimal expTotalOnSolarEnergyConsumption,
            decimal expTotalOffSolarEnergyConsumption,
            decimal expTotalEnergyConsumption)
        {
            var site = new SiteBuilder().Build();
            for (int i = 0; i < inApplianceUsages.Count; ++i)
            {
                // Add a new Applianceusage
                var appliance = new ApplianceBuilder().Build();
                site.Consumption.AddApplianceUsage(appliance);
                // Modify it based on the passed test data
                var applianceUsage = site.Consumption.ApplianceUsages.Last();
                // Rem the default usage interval
                applianceUsage.ApplianceUsageSchedule.ClearUsageIntervals();
                var inApplianceUsage = inApplianceUsages[i];
                applianceUsage.SetQuantity(inApplianceUsage.Quantity);
                applianceUsage.SetPowerConsumption(
                    inApplianceUsage.PowerConsumption);
                applianceUsage.SetNumHoursOnSolar(
                    inApplianceUsage.NumHoursOnSolar);
                applianceUsage.SetNumHoursOffSolar(
                    inApplianceUsage.NumHoursOffSolar);
                foreach(var uti in inApplianceUsage.UsageIntervals)
                {
                    var ti = uti.TimeInterval;
                    int startHr = ti.Start.Hours, startMin = ti.Start.Minutes;
                    int endHr   = ti.End.Hours,   endMin   = ti.End.Minutes;
                    applianceUsage.ApplianceUsageSchedule.AddUsageInterval(
                        startHr, startMin, endHr, endMin, uti.UsageKind
                    );
                }
            }
            site.Consumption.Recalculate();

            Assert.Equal(expMaxPowerConsumption, 
                site.Consumption.ConsumptionTotal.MaxPowerConsumption);
            Assert.Equal(expTotalPowerConsumption,
                site.Consumption.ConsumptionTotal.TotalPowerConsumption);
            Assert.Equal(expTotalOnSolarEnergyConsumption,
                site.Consumption.ConsumptionTotal
                .TotalOnSolarEnergyConsumption);
            Assert.Equal(expTotalOffSolarEnergyConsumption,
                site.Consumption.ConsumptionTotal
                .TotalOffSolarEnergyConsumption);
            Assert.Equal(expTotalEnergyConsumption,
                site.Consumption.ConsumptionTotal.TotalEnergyConsumption);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                // All zeros
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(0, 0, 0, 0,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,8,0)
                                )
                            }
                        ),
                        new CompactApplianceUsage(2, 0, 0, 0,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,8,0)
                                )
                            }
                        ),
                        new CompactApplianceUsage(0, 20.6m, 0, 0,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,8,0)
                                )
                            }
                        ),
                        new CompactApplianceUsage(0, 0, 3, 3,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,11,0)
                                )
                            }
                        ),
                        new CompactApplianceUsage(0, 20.6m, 3, 3,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,11,0)
                                )
                            }
                        ),
                        new CompactApplianceUsage(2, 0, 3, 3,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,11,0)
                                )
                            }
                        )
                    },
                    0, 0, 0, 0, 0
                },
                // Clones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(2, 20, 3, 2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(9,0,12,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(12,0,14,0),
                                    UsageKind.UsingMains
                                )
                            }),
                        new CompactApplianceUsage(2, 20, 3, 2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,9,30)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(9,30,11,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(11,0,13,0),
                                    UsageKind.UsingMains
                                )
                            }),
                        new CompactApplianceUsage(2, 20, 3, 2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(5,0,5,30),
                                    UsageKind.UsingBattery
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(6,0,7,0),
                                    UsageKind.UsingBattery
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,11,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(11,0,11,30),
                                    UsageKind.UsingMains
                                )
                            })
                    },
                    120, 120, 360, 240, 600
                },
                // Different ones
                new object[] {
                    new List<CompactApplianceUsage>{
                        new CompactApplianceUsage(1, 20, 3, 1,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,11,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(16,0,17,0),
                                    UsageKind.UsingBattery
                                )
                            }),
                        new CompactApplianceUsage(2, 20, 3, 2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,8,30)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(9,0,9,30)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(10,0,12,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(16,0,18,0),
                                    UsageKind.UsingBattery
                                )
                            }),
                        new CompactApplianceUsage(3, 20.6m, 4, 5,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(10,0,11,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(12,0,15,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(16,0,21,0),
                                    UsageKind.UsingBattery
                                )
                            })
                    },
                    121.8, 121.8, 427.2, 409, 836.2
                },
                // Terraced
                new object[] {
                    new List<CompactApplianceUsage>() {
                        new CompactApplianceUsage(2,500,2,2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(8,0,10,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(14,0,16,0),
                                    UsageKind.UsingMains
                                )
                            }
                        ),
                        new CompactApplianceUsage(2,500,2,2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(10,0,12,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(14,0,16,0),
                                    UsageKind.UsingMains
                                )
                            }
                        ),
                        new CompactApplianceUsage(2,500,2,2,
                            new List<UsageTimeInterval>() {
                                new UsageTimeInterval(
                                    new TimeInterval(12,0,14,0)
                                ),
                                new UsageTimeInterval(
                                    new TimeInterval(14,0,16,0),
                                    UsageKind.UsingMains
                                )
                            }
                        )
                    },
                    1000,3000,6000,6000,12000
                }
            };
        
        public class CompactApplianceUsage
        {
            public int Quantity { get; set; }
            public decimal PowerConsumption { get; set; }
            public int NumHoursOnSolar { get; set; }
            public int NumHoursOffSolar { get; set; }

            public List<UsageTimeInterval> UsageIntervals { get; set; }

            public CompactApplianceUsage(int quantity, decimal powerConsumption,
                int numHoursOnSolar, int numHoursOffSolar,
                List<UsageTimeInterval> usageIntervals)
            {
                Quantity = quantity;
                PowerConsumption = powerConsumption;
                NumHoursOnSolar = numHoursOnSolar;
                NumHoursOffSolar = numHoursOffSolar;
                UsageIntervals = usageIntervals;
            }
        }
    }
}