using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class RespostaAlunosConfiguration : IEntityTypeConfiguration<RespostaAluno>
    {
        public void Configure(EntityTypeBuilder<RespostaAluno> builder)
        {
            builder.HasOne(respostaAluno => respostaAluno.Avaliacao)
                .WithMany(avaliacao => avaliacao.RespostaAlunos)
                .HasForeignKey(respostaAluno => respostaAluno.AvaliacaoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(respostaAluno => respostaAluno.Aluno)
                .WithMany(aluno => aluno.RespostaAlunos)
                .HasForeignKey(respostaAluno => respostaAluno.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(respostaAluno => respostaAluno.Alternativa)
                .WithOne(aluno => aluno.RespostaAluno)
                .HasForeignKey<RespostaAluno>(respostaAluno => respostaAluno.AlternativaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(respostaAluno => new
            {
                respostaAluno.AvaliacaoId, respostaAluno.AlunoId, respostaAluno.AlternativaId
            });
        }
    }
}