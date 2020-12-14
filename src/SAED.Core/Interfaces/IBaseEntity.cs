﻿using System;

namespace SAED.Core.Interfaces
{
    public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<int>
    {
    }
}