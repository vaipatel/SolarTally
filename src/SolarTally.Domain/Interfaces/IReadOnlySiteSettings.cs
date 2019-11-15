using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.Interfaces
{
    public interface IReadOnlySiteSettings
    {
        int NumSolarHours { get; }
        TimeInterval PeakSolarInterval { get; }
    }
}