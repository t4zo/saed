using System;

namespace SAED.Core.Entities
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}