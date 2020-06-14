using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.ApplicationCore.Entities;

namespace SAED.Infrastructure.Config
{
    public class EtapaDescritorConfiguration : IEntityTypeConfiguration<EtapaDescritor>
    {
        public void Configure(EntityTypeBuilder<EtapaDescritor> builder)
        {
            builder.HasOne(anoDescritor => anoDescritor.Etapa)
                .WithMany(ano => ano.EtapaDescritores)
                .HasForeignKey(anoDescritor => anoDescritor.EtapaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(anoDescritor => anoDescritor.Descritor)
                .WithMany(descritor => descritor.EtapaDescritores)
                .HasForeignKey(anoDescritor => anoDescritor.DescritorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(anoDescritor => new { anoDescritor.EtapaId, anoDescritor.DescritorId });

            #region Outra forma de mapear m2m
            //.HasMany<EscolaPessoa>(e => e.Pessoas)
            //.WithMany(e => e)
            //.Map(x =>
            //{
            //    x.MapLeftKey("Account_Id");
            //    x.MapRightKey("Product_Id");
            //    x.ToTable("AccountProducts");
            //});
            #endregion


        }
    }
}
