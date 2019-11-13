using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.Interfaces
{
    public interface IConsumptionCalculator
    {
        int GetSiteNumSolarHours();

        TimeInterval GetPeakSolarInterval();
        
        void Recalculate();
    }
}