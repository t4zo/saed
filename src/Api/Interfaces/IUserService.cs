using SAED.Api.Entities.Dto;
using System.Threading.Tasks;

namespace SAED.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserRequest> AuthenticateAsync(string username, string password, bool remember);
    }
}
