using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SAED.ApplicationCore.Constants;
using SAED.ApplicationCore.Entities;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Identity;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SAED.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _databaseProvider;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _databaseProvider = configuration[DatabaseConstants.DATABASE];
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
        public DbSet<AvaliacaoQuestao> AvaliacaoQuestoes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var entities = builder.Model.GetEntityTypes();

            foreach (var entity in entities)
            {
                if (_databaseProvider == DatabaseConstants.Postgres && !(entity is IManyToMany))
                {
                    entity.FindProperty("Id")?.SetIdentityStartValue(DatabaseConstants.StartIdValue);
                }

                entity.AddProperty("CreatedBy", typeof(string));
                entity.AddProperty("UpdatedBy", typeof(string));
                entity.AddProperty("CreatedDate", typeof(DateTime));
                entity.AddProperty("UpdatedDate", typeof(DateTime));
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_databaseProvider == DatabaseConstants.Postgres)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultPostgresConnection"));
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            
            try
            {
                foreach (var entityEntry in entries)
                {
                    entityEntry.Property("UpdatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value ?? "Desconhecido";
                    entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("CreatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value ?? "Desconhecido";
                        entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    }
                }
            }
            catch { }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            try
            {
                foreach (var entityEntry in entries)
                {
                    entityEntry.Property("UpdatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value ?? "Desconhecido";
                    entityEntry.Property("UpdatedDate").CurrentValue = DateTime.Now;

                    if (entityEntry.State == EntityState.Added)
                    {
                        entityEntry.Property("CreatedBy").CurrentValue = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value ?? "Desconhecido";
                        entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                    }
                }
            }
            catch { }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        //private void UseHiLoStartingSequence(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasSequence<int>("DbHiLoSequence").StartsAt(1000).IncrementsBy(1);
        //    NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "DbHiLoSequence");
        //}
    }
}
