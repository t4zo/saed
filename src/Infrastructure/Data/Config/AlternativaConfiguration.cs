using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class AlternativaConfiguration : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(alternativa => alternativa.Questao)
                .WithMany(questao => questao.Alternativas)
                .HasForeignKey(alternativa => alternativa.QuestaoId);
        }
    }
}
