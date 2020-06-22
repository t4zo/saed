using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(x => x.Zona)
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(x => x.Distancia)
                .IsRequired();
        }
    }
}
