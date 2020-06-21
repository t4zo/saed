using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class TurmaAlunosConfiguration : IEntityTypeConfiguration<TurmaAluno>
    {
        public void Configure(EntityTypeBuilder<TurmaAluno> builder)
        {
            builder
                .HasOne(turmaAluno => turmaAluno.Turma)
                .WithMany(turma => turma.TurmaAlunos)
                .HasForeignKey(turmaAluno => turmaAluno.TurmaId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder
                .HasOne(turmaAluno => turmaAluno.Aluno)
                .WithMany(aluno => aluno.TurmaAlunos)
                .HasForeignKey(turmaAluno => turmaAluno.AlunoId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasKey(turmaAluno => new { turmaAluno.TurmaId, turmaAluno.AlunoId });
        }
    }
}
