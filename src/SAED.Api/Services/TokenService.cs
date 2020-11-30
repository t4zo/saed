using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAED.Api.Configurations;
using SAED.Core.Interfaces;

namespace SAED.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppConfiguration _appConfiguration;

        public TokenService(IOptionsMonitor<AppConfiguration> appConfiguration)
        {
            _appConfiguration = appConfiguration.CurrentValue;
        }

        string ITokenService.GenerateJWTToken(ClaimsIdentity claimsIdentity)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Issuer = _appConfiguration.Token.Issuer,
                Audience = _appConfiguration.Token.Audience,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(_appConfiguration.Token.Expiration),
                SigningCredentials = _appConfiguration.Token.SigningCredentials
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}