using System.Security.Claims;

namespace SAED.Api.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(ClaimsIdentity claimsIdentity);
    }
}
