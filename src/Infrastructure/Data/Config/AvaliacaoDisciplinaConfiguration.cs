using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class AvaliacaoDisciplinaConfiguration : IEntityTypeConfiguration<AvaliacaoDisciplina>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoDisciplina> builder)
        {
            builder
                .HasOne(disciplinaAvaliacao => disciplinaAvaliacao.Disciplina)
                .WithMany(disciplina => disciplina.AvaliacaoDisciplinas)
                .HasForeignKey(disciplinaAvaliacao => disciplinaAvaliacao.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(disciplinaAvaliacao => disciplinaAvaliacao.Avaliacao)
                .WithMany(avaliacao => avaliacao.AvaliacaoDisciplinas)
                .HasForeignKey(disciplinaAvaliacao => disciplinaAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);            

            builder.HasKey(avaliacaoTurma => new { avaliacaoTurma.DisciplinaId, avaliacaoTurma.AvaliacaoId });
        }
    }
}
