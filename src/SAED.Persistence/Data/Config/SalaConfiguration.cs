using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.Config
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired();

            builder.Property(x => x.Capacidade).IsRequired();

            builder.Property(x => x.Area);

            builder.HasOne(sala => sala.Escola)
                .WithMany(escola => escola.Salas)
                .HasForeignKey(sala => sala.EscolaId);
        }
    }
}