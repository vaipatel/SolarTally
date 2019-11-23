using SolarTally.Domain.Common;
using SolarTally.Domain.Enumerations;

namespace SolarTally.Domain.Interfaces
{
    /// <summary>
    /// Represents some key methods to be implemented by an
    /// ApplianceUsageSchedule
    /// </summary>
    /// <remarks>
    /// Uses the template method pattern to do recurrent pre/post processing,
    /// particularly the calling of RecalculateTotalTimes() after every func.
    /// </remarks>
    public abstract class IApplianceUsageSchedule : BaseEntity<int>
    {
        protected abstract void _ClearUsageIntervals();
        public void ClearUsageIntervals()
        {
            _ClearUsageIntervals();
            RecalculateTotalTimes();
        }

        protected abstract void _AddUsageInterval(int startHr, int startMin, 
            int endHr, int endMin, UsageKind usageKind);
        public void AddUsageInterval(
            int startHr, int startMin, int endHr, int endMin,
            UsageKind usageKind
        )
        {
            _AddUsageInterval(startHr, startMin, endHr, endMin, usageKind);
            RecalculateTotalTimes();
        }

        protected abstract void _HandlePeakSolarIntervalUpdated();
        public void HandlePeakSolarIntervalUpdated()
        {
            _HandlePeakSolarIntervalUpdated();
            RecalculateTotalTimes();
        }

        public abstract void RecalculateTotalTimes();
    }   
}