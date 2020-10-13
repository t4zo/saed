using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SAED.Infrastructure.Data.Seed
{
    public class EntitySeed<TEntity> : IEntitySeed<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EntitySeed(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public virtual void Load(IEnumerable<TEntity> entities, string tableName)
        {
            var dbSet = _context.Set<TEntity>();
            var provider = _configuration[Providers.PROVIDER];

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
                    if (provider != Providers.DigitalOcean)
                    {
                        if (!(entity is IManyToMany))
                        {
                            _context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON;");
                        }
                    }

                    _context.SaveChanges();

                    if (provider != Providers.DigitalOcean)
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
