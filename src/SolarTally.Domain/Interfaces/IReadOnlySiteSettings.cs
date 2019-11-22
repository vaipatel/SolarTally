using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.Interfaces
{
    public interface IReadOnlySiteSettings
    {
        decimal NumSolarHours { get; }
        TimeInterval PeakSolarInterval { get; }
    }
}