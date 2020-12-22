using System;

namespace SAED.Core.Interfaces
{
    public interface IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public interface IEntity : IEntity<int>
    {
    }
}