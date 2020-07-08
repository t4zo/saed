using AutoMapper;
using SAED.Api.Entities.Dto;
using SAED.Api.Entities.Responses;

namespace SAED.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, UserResponse>();
        }
    }
}
