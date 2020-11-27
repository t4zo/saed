using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class TurmaAlunosConfiguration : IEntityTypeConfiguration<TurmaAluno>
    {
        public void Configure(EntityTypeBuilder<TurmaAluno> builder)
        {
            builder
                .HasOne(turmaAluno => turmaAluno.Turma)
                .WithMany(turma => turma.TurmaAlunos)
                .HasForeignKey(turmaAluno => turmaAluno.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(turmaAluno => turmaAluno.Aluno)
                .WithMany(aluno => aluno.TurmaAlunos)
                .HasForeignKey(turmaAluno => turmaAluno.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(turmaAluno => new {turmaAluno.TurmaId, turmaAluno.AlunoId});
        }
    }
}