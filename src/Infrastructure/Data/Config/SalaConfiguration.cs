using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.HasOne(sala => sala.Escola)
                .WithMany(escola => escola.Salas)
                .HasForeignKey(sala => sala.EscolaId);

        }
    }
}
