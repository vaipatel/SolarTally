using System.Collections.Generic;
using System.Linq;
using SolarTally.Domain.Common;
using SolarTally.Domain.Interfaces;
using Ardalis.GuardClauses;

namespace SolarTally.Domain.Entities
{
    public class Consumption : BaseEntity<int>, IAggregateRoot
    {
        private readonly List<ApplianceUsage> _applianceUsages;
        public IReadOnlyCollection<ApplianceUsage> ApplianceUsages =>
            _applianceUsages;

        public Consumption()
        {
            _applianceUsages = new List<ApplianceUsage>();
        }
    }
}