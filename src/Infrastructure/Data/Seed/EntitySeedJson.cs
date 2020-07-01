using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SAED.Infrastructure.Data.Seed
{
    public abstract class EntitySeedJson<TEntity> : IEntitySeedJson where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected string _ressourceName { get; set; }

        protected EntitySeedJson(ApplicationDbContext context, string ressourceName)
        {
            _context = context;
            _ressourceName = ressourceName;
        }

        public virtual async Task LoadAsync()
        {
            var dbSet = _context.Set<TEntity>();

            if (!dbSet.Any())
            {
                var assembly = Assembly.GetExecutingAssembly();

                using var stream = assembly.GetManifestResourceStream(_ressourceName);
                using var reader = new StreamReader(stream, Encoding.UTF8);

                string json = await reader.ReadToEndAsync();
                List<TEntity> entities = JsonSerializer.Deserialize<List<TEntity>>(json);

                await dbSet.AddRangeAsync(entities);

                await _context.SaveChangesAsync();
            }
        }
    }
}
