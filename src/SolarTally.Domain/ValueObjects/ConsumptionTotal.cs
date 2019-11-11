using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Entities;

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
}
}