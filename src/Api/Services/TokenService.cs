using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAED.Api.Configurations;
using SAED.ApplicationCore.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SAED.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppConfiguration _appConfiguration;

        public TokenService(IOptionsMonitor<AppConfiguration> options)
        {
            _appConfiguration = options.Get("AppConfiguration");
        }

        string ITokenService.GenerateJWTToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _appConfiguration.Token.Issuer,
                Audience = _appConfiguration.Token.Audience,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(_appConfiguration.Token.Expiration),
                SigningCredentials = _appConfiguration.Token.SigningCredentials
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
