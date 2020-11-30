using System.Security.Claims;

namespace SAED.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(ClaimsIdentity claimsIdentity);
    }
}