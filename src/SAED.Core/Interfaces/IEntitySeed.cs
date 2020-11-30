using System.Collections.Generic;

namespace SAED.Core.Interfaces
{
    public interface IEntitySeed<TEntity> where TEntity : class
    {
        void Load(IEnumerable<TEntity> entities, string tableName);
    }
}