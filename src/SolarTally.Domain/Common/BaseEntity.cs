using System;
using System.Collections.Generic;
using MediatR;

namespace SolarTally.Domain.Common
{
    /// <summary>
    /// Base class with Id and Events functionality.
    ///
    /// Most of the domain entities will be derived from here. T can be int or
    /// Guid.
    /// </summary>
    /// <remarks>
    /// I've stolen most of the below from the
    /// <a href="https://bit.ly/33Qe0l9">Entity class in the SeedWork folder
    /// of the Ordering.Domain project in eShopOnContainers.</a>
    /// The key difference is that I've genericized the class to accept int or
    /// Guid.
    ///
    /// TODO: Perhaps I can constrain T to be IComparable, IComparable<T> etc.
    /// </remarks>
    public abstract class BaseEntity<T>
    {
        int? _requestedHashCode;
        int _Id;        
        public virtual int Id 
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return this.Id.Equals(default(T));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity<T>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            BaseEntity<T> item = (BaseEntity<T>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id.Equals(this.Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    // XOR for random distribution
                    // (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(BaseEntity<T> left, BaseEntity<T> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<T> left, BaseEntity<T> right)
        {
            return !(left == right);
        }
    }
}