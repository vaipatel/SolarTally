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
    public decimal TotalEnergyConsumption { get; private set; }
    public decimal TotalNonSolarEnergyConsumption { get; private set; }

    private ConsumptionTotal() { }

    public ConsumptionTotal(Consumption consumption)
    {
        TotalPowerConsumption = 0;
        TotalEnergyConsumption = 0;
        TotalNonSolarEnergyConsumption = 0;
        foreach(var au in consumption.ApplianceUsages)
        {
            TotalPowerConsumption += 
                au.ApplianceUsageTotal.TotalPowerConsumption;
            TotalEnergyConsumption +=
                au.ApplianceUsageTotal.TotalEnergyConsumption;
            TotalNonSolarEnergyConsumption +=
                au.ApplianceUsageTotal.TotalNonSolarEnergyConsumption;
        }
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        // Using a yield return statement to return each element one at a time
        yield return TotalPowerConsumption;
        yield return TotalEnergyConsumption;
        yield return TotalNonSolarEnergyConsumption;
    }
}
}