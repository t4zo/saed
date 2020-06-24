using AutoMapper;
using SAED.Api.Entities.Dto;
using SAED.Infrastructure.Identity;

namespace SAED.Api.Utilities
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
