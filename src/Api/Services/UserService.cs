using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SAED.Api.Configurations;
using SAED.Api.Entities.Dto;
using SAED.Api.Interfaces;
using SAED.ApplicationCore.Constants;
using SAED.Infrastructure.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SAED.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppConfiguration _appConfiguration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(
            IOptionsMonitor<AppConfiguration> options,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper
            )
        {
            _appConfiguration = options.Get("AppConfiguration");
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<UserDto> AuthenticateAsync(string username, string password, bool remember)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: remember, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var roles = await _userManager.GetRolesAsync(user);

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appConfiguration.Token.SecurityKey);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(AuthorizationConstants.Remember, remember.ToString().ToLower())
                    });

                claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claimsIdentity,
                    Issuer = _appConfiguration.Token.Issuer,
                    Audience = _appConfiguration.Token.Audience,
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                var userDto = _mapper.Map<UserDto>(user);

                userDto.Roles = roles;
                userDto.Token = token;

                return userDto.WithoutPassword();
            }

            return null;
        }
    }
}
