namespace SolarTally.Domain.Common
{
    /// T can be int or Guid
    public class BaseEntity<T>
    {
        public T Id { get; set; }
    }
}