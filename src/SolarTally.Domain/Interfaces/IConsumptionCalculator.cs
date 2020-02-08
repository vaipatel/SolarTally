using SolarTally.Domain.Entities;

namespace SolarTally.Domain.Interfaces
{
    public interface IConsumptionCalculator
    {
        IReadOnlySiteSettings ReadOnlySiteSettings { get; }
        void Recalculate();
    }
}