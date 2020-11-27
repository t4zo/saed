using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Data.Config
{
    public class FormaConfiguration : IEntityTypeConfiguration<Forma>
    {
        public void Configure(EntityTypeBuilder<Forma> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasMaxLength(16)
                .IsRequired();
        }
    }
}