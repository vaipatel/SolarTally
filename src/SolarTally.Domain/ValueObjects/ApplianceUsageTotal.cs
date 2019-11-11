using System.Collections.Generic;
using SolarTally.Domain.Common;
using SolarTally.Domain.Interfaces;

namespace SolarTally.Domain.ValueObjects
{
    /// <summary>
    /// Encapsulates the calculated totals for a single ApplianceUsage record,
    /// given the quantity, power consumption in watts and hrs of operations.
    /// </summary>
    public class ApplianceUsageTotal : ValueObject
{
    public decimal TotalPowerConsumption { get; private set; }
    public decimal TotalOnSolarEnergyConsumption { get; private set; }
    public decimal TotalOffSolarEnergyConsumption { get; private set; }
    public decimal TotalEnergyConsumption { get; private set; }

    private ApplianceUsageTotal() { }

    public ApplianceUsageTotal(IApplianceUsageInfo applianceUsageInfo)
    {
        var quantity = applianceUsageInfo.GetQuantity();
        var powerConsumption = applianceUsageInfo.GetPowerConsumption();
        var numHours = applianceUsageInfo.GetNumHours();
        var numHoursOnSolar = applianceUsageInfo.GetNumHoursOnSolar();
        var numHoursOffSolar = applianceUsageInfo.GetNumHoursOffSolar();
        TotalPowerConsumption = quantity * powerConsumption;
        TotalOnSolarEnergyConsumption = TotalPowerConsumption * numHoursOnSolar;
        TotalOffSolarEnergyConsumption = TotalPowerConsumption *
            numHoursOffSolar;
        // TODO: Probably just add TotalOnSolarEnergy + TotalOffSolarEnergy
        TotalEnergyConsumption = TotalPowerConsumption * numHours;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        // Using a yield return statement to return each element one at a time
        yield return TotalPowerConsumption;
        yield return TotalEnergyConsumption;
        yield return TotalOffSolarEnergyConsumption;
    }
}
}