using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class AvaliacaoDisciplinaConfiguration : IEntityTypeConfiguration<AvaliacaoDisciplinaEtapa>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoDisciplinaEtapa> builder)
        {
            builder
                .HasOne(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.Avaliacao)
                .WithMany(avaliacao => avaliacao.AvaliacaoDisciplinasEtapas)
                .HasForeignKey(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.Disciplina)
                .WithMany(disciplina => disciplina.AvaliacaoDisciplinasEtapas)
                .HasForeignKey(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.Etapa)
                .WithMany(etapa => etapa.AvaliacaoDisciplinasEtapas)
                .HasForeignKey(avaliacaoDisciplinaEtapa => avaliacaoDisciplinaEtapa.EtapaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(avaliacaoDisciplinaEtapa =>
                new
                {
                    avaliacaoDisciplinaEtapa.DisciplinaId,
                    avaliacaoDisciplinaEtapa.AvaliacaoId,
                    avaliacaoDisciplinaEtapa.EtapaId
                });
        }
    }
}