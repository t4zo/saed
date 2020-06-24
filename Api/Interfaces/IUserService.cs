using SAED.Api.Entities.Dto;
using System.Threading.Tasks;

namespace SAED.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AuthenticateAsync(string username, string password, bool remember);
    }
}
