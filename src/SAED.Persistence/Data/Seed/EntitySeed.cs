using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SAED.Persistence.Data.Seed
{
    public class EntitySeed<TEntity> : IEntitySeed<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly string _databaseProvider;

        public EntitySeed(ApplicationDbContext context, string databaseProvider)
        {
            _context = context;
            _databaseProvider = databaseProvider;
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
                    if (_databaseProvider != DatabaseConstants.Postgres)
                    {
                        if (!(entity is IManyToMany))
                        {
                            _context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON;");
                        }
                    }

                    _context.SaveChanges();

                    if (_databaseProvider != DatabaseConstants.Postgres)
                    {
                        if (!(entity is IManyToMany))
                        {
                            _context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} OFF;");
                        }
                    }
                }

                transaction.Commit();
            }
        }
    }
}