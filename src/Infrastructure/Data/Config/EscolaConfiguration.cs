using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class EscolaConfiguration : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(escola => escola.Distrito)
                .WithMany(distrito => distrito.Escolas)
                .HasForeignKey(escola => escola.DistritoId);

            builder.HasKey(escola => escola.Id);

            builder.Property(escola => escola.Nome).IsRequired();
        }
    }
}
