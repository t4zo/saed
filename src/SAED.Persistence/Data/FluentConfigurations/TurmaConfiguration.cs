using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.FluentConfigurations
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(32)
                .IsRequired();

            builder.HasOne(turma => turma.Sala)
                .WithMany(sala => sala.Turmas)
                .HasForeignKey(turma => turma.SalaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(turma => turma.Etapa)
                .WithMany(etapa => etapa.Turmas)
                .HasForeignKey(turma => turma.EtapaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(turma => turma.Turno)
                .WithMany(turno => turno.Turmas)
                .HasForeignKey(turma => turma.TurnoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(turma => turma.Forma)
                .WithMany(forma => forma.Turmas)
                .HasForeignKey(turma => turma.FormaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}