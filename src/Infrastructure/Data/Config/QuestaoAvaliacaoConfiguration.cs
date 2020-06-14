using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class QuestaoAvaliacaoConfiguration : IEntityTypeConfiguration<QuestaoAvaliacao>
    {
        public void Configure(EntityTypeBuilder<QuestaoAvaliacao> builder)
        {
            builder.HasOne(questaoAvaliacao => questaoAvaliacao.Avaliacao)
                .WithMany(questao => questao.QuestaoAvaliacoes)
                .HasForeignKey(questaoAvaliacao => questaoAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(questaoAvaliacao => questaoAvaliacao.Questao)
                .WithMany(questao => questao.QuestaoAvaliacoes)
                .HasForeignKey(QuestaoAvaliacao => QuestaoAvaliacao.QuestaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(questaoAvaliacao => new { questaoAvaliacao.AvaliacaoId, questaoAvaliacao.QuestaoId });
        }
    }
}
