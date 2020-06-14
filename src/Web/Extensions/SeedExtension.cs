using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SAED.Infrastructure.Data;
using SAED.ApplicationCore.Entities;
using System;
using System.Threading.Tasks;

namespace SAED.Web.Extensions
{
    public static class SeedExtension
    {
        public static async Task<IApplicationBuilder> SeedDatabase(
            this IApplicationBuilder app,
            IServiceProvider serviceProvider
            )
        {
            var context = serviceProvider.GetRequiredService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            //await new AvaliacoesSeed(context).LoadAsync();

            //await new DistritosSeed(context).LoadAsync();
            //await new EscolasSeed(context).LoadAsync();

            //await new CursosSeed(context).LoadAsync();
            //await new SegmentosSeed(context).LoadAsync();
            //await new EtapasSeed(context).LoadAsync();

            //await new TurnosSeed(context).LoadAsync();
            //await new FormasSeed(context).LoadAsync();

            //await new SalasSeed(context).LoadAsync();
            //await new TurmasSeed(context).LoadAsync();
            //await new AlunosSeed(context).LoadAsync();

            //await new DisciplinasSeed(context).LoadAsync();
            //await new TemasSeed(context).LoadAsync();
            //await new DescritoresSeed(context).LoadAsync();
            //await new QuestoesSeed(context).LoadAsync();
            //await new AlternativasSeed(context).LoadAsync();

            //await new UsuarioTurmaAvaliacoesSeed(context).LoadAsync();
            //await new AvaliacaoDisciplinasSeed(context).LoadAsync();
            //await new AvaliacaoDistritosSeed(context).LoadAsync();
            //await new TurmaAlunosSeed(context).LoadAsync();
            //await new RespostaAlunosSeed(context).LoadAsync();
            //await new EtapaDescritoresSeed(context).LoadAsync();
            //await new QuestaoAvaliacoesSeed(context).LoadAsync();

            return app;
        }
    }
}
