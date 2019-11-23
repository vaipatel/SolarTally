using SolarTally.Domain.Common;

namespace SolarTally.Domain.Interfaces
{
    /// <summary>
    /// Represents some key methods to be implemented by an ApplianceUsage
    /// </summary>
    /// <remarks>
    /// Uses the template method pattern to do recurrent pre/post processing,
    /// particularly the calling of Recalculate() after every func.
    /// </remarks>
    public abstract class IApplianceUsage : BaseEntity<int>
    {
        public void SetQuantity(int quantity)
        {
            _SetQuantity(quantity);
            Recalculate();
        }

        public void SetPowerConsumption(decimal powerConsumption)
        {
            _SetPowerConsumption(powerConsumption);
            Recalculate();
        }

        public void SetPowerConsumptionToDefault()
        {
            _SetPowerConsumptionToDefault();
            Recalculate();
        }

        public void SetEnabled(bool enabled)
        {
            _SetEnabled(enabled);
            Recalculate();
        }

        public void HandleSolarIntervalUpdated()
        {
            _HandleSolarIntervalUpdated();
            Recalculate();
        }

        protected abstract void _SetQuantity(int quantity);

        protected abstract void _SetPowerConsumption(decimal powerConsumption);

        protected abstract void _SetPowerConsumptionToDefault();
        
        protected abstract void _SetEnabled(bool enabled);

        protected abstract void _HandleSolarIntervalUpdated();

        public abstract void Recalculate();
    }   
}