using AutoMapper;
using SAED.Core.Entities;
using SAED.Web.Areas.Administrador.ViewModels;

namespace SAED.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Questao, QuestaoViewModel>().ReverseMap();
        }
    }
}