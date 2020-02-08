namespace SolarTally.Domain.Interfaces
{
    /// <summary>
    /// Represents some key information to be coughed up by an ApplianceUsage
    /// </summary>
    public interface IApplianceUsageInfo
    {
        public int GetQuantity();
        public decimal GetPowerConsumption();
        public decimal GetNumHours();
        public decimal GetNumHoursOnSolar();
        public decimal GetNumHoursOffSolar();
    }   
}