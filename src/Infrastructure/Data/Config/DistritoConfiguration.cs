using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class DistritoConfiguration : IEntityTypeConfiguration<Distrito>
    {
        public void Configure(EntityTypeBuilder<Distrito> builder)
        {
            builder.Property(x => x.Id);

            builder.HasKey(distrito => distrito.Id);

            builder.Property(distrito => distrito.Nome).IsRequired();
        }
    }
}
