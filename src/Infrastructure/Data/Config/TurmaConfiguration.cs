using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(turma => turma.Sala)
                .WithMany(sala => sala.Turmas)
                .HasForeignKey(turma => turma.SalaId);

            builder.HasOne(turma => turma.Etapa)
                .WithMany(etapa => etapa.Turmas)
                .HasForeignKey(turma => turma.EtapaId);

            builder.HasOne(turma => turma.Turno)
                .WithMany(turno => turno.Turmas)
                .HasForeignKey(turma => turma.TurnoId);

            builder.HasOne(turma => turma.Forma)
                .WithMany(forma => forma.Turmas)
                .HasForeignKey(turma => turma.FormaId);

            builder.HasKey(turma => turma.Id);
        }
    }
}
