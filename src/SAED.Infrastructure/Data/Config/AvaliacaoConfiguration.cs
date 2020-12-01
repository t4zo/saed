﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SAED.Core.Entities;

namespace SAED.Infrastructure.Data.Config
{
    internal class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //using var stream = assembly.GetManifestResourceStream("SAED.Infrastructure.Data.Seed.Json.00Avaliacoes.json");
            //using var reader = new StreamReader(stream, Encoding.UTF8);

            //string json = reader.ReadToEnd();
            //List<Avaliacao> entities = JsonConvert.DeserializeObject<List<Avaliacao>>(json);

            //builder.HasData(entities);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .IsRequired();
        }
    }
}