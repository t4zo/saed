using Microsoft.EntityFrameworkCore;
using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SAED.Infrastructure.Data.Seed
{
    public class EntitySeed<TEntity> : IEntitySeed<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;

        public EntitySeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual void Load(IEnumerable<TEntity> entities, string tableName)
        {

            var dbSet = _context.Set<TEntity>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(entities);

                using var transaction = _context.Database.BeginTransaction();
                
                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = typeof(TEntity).Name;
                }

                foreach (var entity in entities)
                {
                    if (!(entity is IManyToMany))
                    {
                        _context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON;");
                    }

                    _context.SaveChanges();

                    if (!(entity is IManyToMany))
                    {
                        _context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} OFF;");
                    }
                }

                transaction.Commit();
            }
        }
    }
}
