using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAED.Api.Options;
using SAED.Core.Interfaces;

namespace SAED.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppOptions _appOptions;

        public TokenService(IOptionsMonitor<AppOptions> appConfiguration)
        {
            _appOptions = appConfiguration.CurrentValue;
        }

        string ITokenService.GenerateJWTToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _appOptions.Token.Issuer,
                Audience = _appOptions.Token.Audience,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(_appOptions.Token.Expiration),
                SigningCredentials = _appOptions.Token.SigningCredentials
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}