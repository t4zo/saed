using System.Security.Claims;

namespace SAED.ApplicationCore.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(ClaimsIdentity claimsIdentity);
    }
}
