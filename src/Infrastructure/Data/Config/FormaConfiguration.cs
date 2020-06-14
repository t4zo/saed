using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class FormaConfiguration : IEntityTypeConfiguration<Forma>
    {
        public void Configure(EntityTypeBuilder<Forma> builder)
        {
            builder.Property(x => x.Id);

            builder.HasKey(forma => forma.Id);
        }
    }
}
