using System.Threading.Tasks;
using SAED.Api.Entities.Responses;

namespace SAED.Api.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> AuthenticateAsync(string username, string password, bool remember);
    }
}