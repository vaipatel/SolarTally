using SolarTally.Domain.Interfaces;
using SolarTally.Domain.ValueObjects;

namespace SolarTally.Domain.UnitTests.Entities
{
    public class MockReadOnlySiteSettings : IReadOnlySiteSettings
    {
        private TimeInterval _peakSolarInterval;
        public string Name => "A Site Name";
        public int NumSolarHours => 8;
        public TimeInterval PeakSolarInterval => _peakSolarInterval;

        public MockReadOnlySiteSettings(
            int startHr = 8, int startMin = 0, int endHr = 16, int endMin = 0)
        {
            this.SetPeakSolarInterval(startHr,startMin,endHr,endMin);
        }

        public void SetPeakSolarInterval(
            int startHr, int startMin, int endHr, int endMin)
        {
            _peakSolarInterval =
                new TimeInterval(startHr,startMin,endHr,endMin);
        }
    }
}