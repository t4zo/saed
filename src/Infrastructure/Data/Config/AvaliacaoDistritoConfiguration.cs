using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class AvaliacaoDistritoConfiguration : IEntityTypeConfiguration<AvaliacaoDistrito>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoDistrito> builder)
        {
            builder
                .HasOne(avaliacaoTurma => avaliacaoTurma.Avaliacao)
                .WithMany(avaliacao => avaliacao.AvaliacaoDistritos)
                .HasForeignKey(avaliacaoTurma => avaliacaoTurma.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(avaliacaoTurma => avaliacaoTurma.Distrito)
                .WithMany(turma => turma.AvaliacaoDistritos)
                .HasForeignKey(avaliacaoTurma => avaliacaoTurma.DistritoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(avaliacaoTurma => new { avaliacaoTurma.AvaliacaoId, avaliacaoTurma.DistritoId });
        }
    }
}
