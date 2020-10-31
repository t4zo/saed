using SAED.Api.Entities.Responses;
using System.Threading.Tasks;

namespace SAED.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> AuthenticateAsync(string username, string password, bool remember);
    }
}
