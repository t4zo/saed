using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class EscolaConfiguration : IEntityTypeConfiguration<Escola>
    {
        public void Configure(EntityTypeBuilder<Escola> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Inep)
                .IsRequired(false);

            builder.Property(x => x.MatrizId)
                .IsRequired(false);

            builder.Property(x => x.DistritoId)
                .IsRequired();

            builder.Property(x => x.Bairro)
                .HasMaxLength(128)
                .IsRequired(false);

            builder.Property(x => x.Rua)
                .HasMaxLength(256)
                .IsRequired(false);

            builder.Property(x => x.Numero)
                .IsRequired(false);

            builder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Telefone)
                .HasMaxLength(11)
                .IsRequired(false);

            builder.HasOne(escola => escola.Distrito)
                .WithMany(distrito => distrito.Escolas)
                .HasForeignKey(escola => escola.DistritoId);
        }
    }
}