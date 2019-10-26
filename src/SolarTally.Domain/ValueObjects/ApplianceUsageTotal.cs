using System.Collections.Generic;
using SolarTally.Domain.Common;

namespace SolarTally.Domain.ValueObjects
{
    /// <summary>
    /// Encapsulates the calculated totals for a single ApplianceUsage record,
    /// given the quantity, power consumption in watts and hrs of operations.
    /// </summary>
    public class ApplianceUsageTotal : ValueObject
{
    public decimal TotalPowerConsumption { get; private set; }
    public decimal TotalEnergyConsumption { get; private set; }

    private ApplianceUsageTotal() { }

    public ApplianceUsageTotal(int quantity, decimal powerConsumption,
        int numHours)
    {
        TotalPowerConsumption = quantity * powerConsumption;
        TotalEnergyConsumption = TotalPowerConsumption * numHours;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        // Using a yield return statement to return each element one at a time
        yield return TotalPowerConsumption;
        yield return TotalEnergyConsumption;
    }
}
}