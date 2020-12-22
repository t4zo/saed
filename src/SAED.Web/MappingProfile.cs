using AutoMapper;
using SAED.Core.Entities;
using SAED.Web.Areas.Administrador.ViewModels;
using SAED.Web.Areas.Aplicador.ViewModels;

namespace SAED.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Questao, QuestaoViewModel>().ReverseMap();
            CreateMap<Questao, RespostasViewModel>().ReverseMap();
            CreateMap<Questao, RespostaViewModel>().ReverseMap();
        }
    }
}