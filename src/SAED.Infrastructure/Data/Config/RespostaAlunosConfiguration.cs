﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class RespostaAlunosConfiguration : IEntityTypeConfiguration<RespostaAluno>
    {
        public void Configure(EntityTypeBuilder<RespostaAluno> builder)
        {
            builder.HasOne(respostaAluno => respostaAluno.Avaliacao)
                .WithMany(avaliacao => avaliacao.RespostaAlunos)
                .HasForeignKey(respostaAluno => respostaAluno.AvaliacaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(respostaAluno => respostaAluno.Aluno)
                .WithMany(aluno => aluno.RespostaAlunos)
                .HasForeignKey(respostaAluno => respostaAluno.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(respostaAluno => respostaAluno.Alternativa)
                .WithOne(aluno => aluno.RespostaAluno)
                .HasForeignKey<RespostaAluno>(respostaAluno => respostaAluno.AlternativaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(respostaAluno => new
            {
                respostaAluno.AvaliacaoId, respostaAluno.AlunoId, respostaAluno.AlternativaId
            });
        }
    }
}