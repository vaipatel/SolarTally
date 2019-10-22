using MediatR;
using SolarTally.Domain.Entities;

namespace SolarTally.Domain.Events
{
    public class SiteNumSolarHoursUpdatedDomainEvent
        : INotification
    {
        public Site Site { get; private set; }

        public SiteNumSolarHoursUpdatedDomainEvent(Site site)
        {
            Site = site;
        }
    }
}