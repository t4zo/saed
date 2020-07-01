using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Escola> Escolas { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var entities = builder.Model.GetEntityTypes();

            foreach (var entity in entities)
            {
                entity.AddProperty("CreatedDate", typeof(DateTime));
                entity.AddProperty("UpdatedDate", typeof(DateTime));
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionString = GetConnectionEnvironmentString();

            optionsBuilder.UseSqlServer(connectionString);
        }

        public override int SaveChanges()
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

        private string GetConnectionEnvironmentString()
        {
            var env = _configuration["ASPNETCORE_ENVIRONMENT"];

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            if (env == "Heroku")
            {
                var connUrl = _configuration["DATABASE_URL"];

                // Parse connection URL to connection string for Npgsql
                connUrl = connUrl.Replace("postgres://", string.Empty);

                var pgUserPass = connUrl.Split("@")[0];
                var pgHostPortDb = connUrl.Split("@")[1];
                var pgHostPort = pgHostPortDb.Split("/")[0];

                var pgHost = pgHostPort.Split(":")[0];
                var pgDb = pgHostPortDb.Split("/")[1];
                var pgUser = pgUserPass.Split(":")[0];
                var pgPort = pgHostPort.Split(":")[1];
                var pgPass = pgUserPass.Split(":")[1];

                connectionString = $"Host={pgHost};Database={pgDb};User Id={pgUser};Port={pgPort};Password={pgPass}";
            }

            return connectionString;
        }

        //private void UseHiLoStartingSequence(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasSequence<int>("DbHiLoSequence").StartsAt(1000).IncrementsBy(1);
        //    NpgsqlModelBuilderExtensions.UseHiLo(modelBuilder, "DbHiLoSequence");
        //}
    }
}
