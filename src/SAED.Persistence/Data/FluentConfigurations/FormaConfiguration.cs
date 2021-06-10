using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Persistence.Data.FluentConfigurations
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