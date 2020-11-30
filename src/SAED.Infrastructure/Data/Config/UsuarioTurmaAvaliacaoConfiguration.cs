using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class UsuarioTurmaAvaliacaoConfiguration : IEntityTypeConfiguration<UsuarioTurmaAvaliacao>
    {
        public void Configure(EntityTypeBuilder<UsuarioTurmaAvaliacao> builder)
        {
            builder
                .HasOne(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.Turma)
                .WithMany(escola => escola.UsuarioTurmaAvaliacao)
                .HasForeignKey(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.Avaliacao)
                .WithMany(escola => escola.UsuarioTurmaAvaliacao)
                .HasForeignKey(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(pessoaTurmaAvaliacao => new
            {
                pessoaTurmaAvaliacao.ApplicationUserId,
                pessoaTurmaAvaliacao.TurmaId,
                pessoaTurmaAvaliacao.AvaliacaoId
            });
        }
    }
}