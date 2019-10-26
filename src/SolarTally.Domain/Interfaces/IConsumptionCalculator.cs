namespace SolarTally.Domain.Interfaces
{
    public interface IConsumptionCalculator
    {
        int GetSiteNumSolarHours();
        void Recalculate();
    }
}