using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.FluentConfigurations
{
    public class QuestaoConfiguration : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Item)
                .HasMaxLength(8)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired(false);

            builder.Property(x => x.Enunciado)
                .IsRequired();

            builder.HasOne(questao => questao.Descritor)
                .WithMany(descritor => descritor.Questoes)
                .HasForeignKey(questao => questao.DescritorId);

            builder.HasOne(questao => questao.Etapa)
                .WithMany(etapa => etapa.Questoes)
                .HasForeignKey(questao => questao.EtapaId);
        }
    }
}