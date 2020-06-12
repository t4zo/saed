using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace Infrastructure.Data.Config
{
    class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasData(
                new Avaliacao { Id = 1, Codigo = "2020", Status = StatusAvaliacao.EmAndamento },
                new Avaliacao { Id = 2, Codigo = "2021", Status = StatusAvaliacao.ARealizar },
                new Avaliacao { Id = 3, Codigo = "2022", Status = StatusAvaliacao.ARealizar }
            );
        }
    }
}
