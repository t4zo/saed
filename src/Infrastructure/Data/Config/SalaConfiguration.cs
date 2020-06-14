using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class SalaConfiguration : IEntityTypeConfiguration<Sala>
    {
        public void Configure(EntityTypeBuilder<Sala> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(sala => sala.Escola)
                .WithMany(escola => escola.Salas)
                .HasForeignKey(sala => sala.EscolaId);

            builder.HasKey(sala => sala.Id);
        }
    }
}
