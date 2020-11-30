using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class QuestaoAvaliacaoConfiguration : IEntityTypeConfiguration<AvaliacaoQuestao>
    {
        public void Configure(EntityTypeBuilder<AvaliacaoQuestao> builder)
        {
            builder.HasOne(questaoAvaliacao => questaoAvaliacao.Avaliacao)
                .WithMany(questao => questao.AvaliacaoQuestoes)
                .HasForeignKey(questaoAvaliacao => questaoAvaliacao.AvaliacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(questaoAvaliacao => questaoAvaliacao.Questao)
                .WithMany(questao => questao.AvaliacaoQuestoes)
                .HasForeignKey(QuestaoAvaliacao => QuestaoAvaliacao.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(questaoAvaliacao => new {questaoAvaliacao.AvaliacaoId, questaoAvaliacao.QuestaoId});
        }
    }
}