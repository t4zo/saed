using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class DescritorConfiguration : IEntityTypeConfiguration<Descritor>
    {
        public void Configure(EntityTypeBuilder<Descritor> builder)
        {
            builder.Property(x => x.Id);

            builder.HasOne(descritor => descritor.Tema)
                .WithMany(tema => tema.Descritores)
                .HasForeignKey(descritor => descritor.TemaId);

            builder.HasKey(descritor => descritor.Id);
        }
    }
}
