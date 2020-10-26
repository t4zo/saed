using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.Infrastructure.Identity;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace SAED.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        private readonly IConfiguration _configuration;
        private readonly string _provider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
            _provider = _configuration[Providers.PROVIDER];
        }

        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Segmento> Segmentos { get; set; }
        public DbSet<Etapa> Etapas { get; set; }
        public DbSet<AvaliacaoDisciplinaEtapa> AvaliacaoDisciplinasEtapas { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Descritor> Descritores { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Alternativa> Alternativas { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var entities = builder.Model.GetEntityTypes();

            foreach (var entity in entities)
            {
                //entity.AddProperty("CreatedBy", typeof(int));
                //entity.AddProperty("UpdatedBy", typeof(int));
                entity.AddProperty("CreatedDate", typeof(DateTime));
                entity.AddProperty("UpdatedDate", typeof(DateTime));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (_provider == Providers.DigitalOcean)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultPostgresConnection"));
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                //entityEntry.Property("UpdatedBy").CurrentValue = _userManager.GetUserAsync(HttpContext.User);
                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //private void UseHiLoStartingSequence(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasSequence<int>("DbHiLoSequence").StartsAt(1000).IncrementsBy(1);
        //    NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "DbHiLoSequence");
        //}
    }
}
