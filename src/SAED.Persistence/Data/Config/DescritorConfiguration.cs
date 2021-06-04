using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.Config
{
    public class DescritorConfiguration : IEntityTypeConfiguration<Descritor>
    {
        public void Configure(EntityTypeBuilder<Descritor> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasOne(descritor => descritor.Tema)
                .WithMany(tema => tema.Descritores)
                .HasForeignKey(descritor => descritor.TemaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}