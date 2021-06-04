using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.Config
{
    public class AlternativaConfiguration : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .IsRequired();

            builder.Property(x => x.QuestaoId)
                .IsRequired();

            builder.HasOne(alternativa => alternativa.Questao)
                .WithMany(questao => questao.Alternativas)
                .HasForeignKey(alternativa => alternativa.QuestaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}