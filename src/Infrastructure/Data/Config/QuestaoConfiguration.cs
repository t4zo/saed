using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class QuestaoConfiguration : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(questao => questao.Descritor)
                .WithMany(descritor => descritor.Questoes)
                .HasForeignKey(questao => questao.DescritorId);

            builder.HasKey(questao => questao.Id);
        }
    }
}
