using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Interfaces;

namespace SAED.Infrastructure.Data.Seed
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
            DbSet<TEntity> dbSet = _context.Set<TEntity>();

            if (!dbSet.Any())
            {
                dbSet.AddRange(entities);

                using IDbContextTransaction transaction = _context.Database.BeginTransaction();

                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = typeof(TEntity).Name;
                }

                foreach (TEntity entity in entities)
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