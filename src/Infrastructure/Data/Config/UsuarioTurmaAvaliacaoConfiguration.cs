using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class UsuarioTurmaAvaliacaoConfiguration : IEntityTypeConfiguration<UsuarioTurmaAvaliacao>
    {
        public void Configure(EntityTypeBuilder<UsuarioTurmaAvaliacao> builder)
        {

            builder
                .HasOne(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.Turma)
                .WithMany(escola => escola.UsuarioTurmaAvaliacao)
                .HasForeignKey(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.Avaliacao)
                .WithMany(escola => escola.UsuarioTurmaAvaliacao)
                .HasForeignKey(pessoaTurmaAvaliacao => pessoaTurmaAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(pessoaTurmaAvaliacao => new
            {
                pessoaTurmaAvaliacao.ApplicationUserId,
                pessoaTurmaAvaliacao.TurmaId,
                pessoaTurmaAvaliacao.AvaliacaoId
            });
        }
    }
}
