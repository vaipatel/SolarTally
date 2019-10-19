using System.Collections.Generic;
using Ardalis.GuardClauses;
using SolarTally.Domain.Common;

namespace SolarTally.Domain.ValueObjects
{
    public class LoadItem : ValueObject
    {
        public string Name { get; private set; }

        public string Description { get; private set; }
        //public byte? Image { get; private set; }

        public int Quantity { get; private set; }

        /// <value>The amount of power consumed by the LoadItem in kW.</value>
        public decimal PowerRating { get; private set; }

        public int NumHours { get; private set; }

        public bool Enabled { get; private set; }

        private LoadItem()
        {
            // required by EF
        }
        
        public LoadItem(string name, string description, int quantity,
                        decimal powerRating, int numHours, bool enabled)
        {
            Guard.Against.LessThan(quantity, nameof(quantity), 0);
            Guard.Against.LessThan(numHours, nameof(numHours), 0);

            Name = name;
            Description = description;
            Quantity = quantity;
            PowerRating = powerRating;
            NumHours = numHours;
            Enabled = enabled;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Description;
            yield return Quantity;
            yield return PowerRating;
            yield return NumHours;
            yield return Enabled;
        }
    }
}