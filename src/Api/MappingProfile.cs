using AutoMapper;
using SAED.Api.Entities.Dto;
using SAED.Api.Entities.Responses;
using SAED.Infrastructure.Identity;

namespace SAED.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserRequest>().ReverseMap();
            CreateMap<ApplicationUser, UserResponse>().ReverseMap();
        }
    }
}