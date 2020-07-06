using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SAED.Api.Entities.Dto;
using SAED.Api.Interfaces;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

namespace SAED.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserService(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper
            )
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<UserRequest> AuthenticateAsync(string username, string password, bool remember)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent: remember, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                var claimsIdentity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(Remember, remember.ToString().ToLower())
                    });

                claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                var token = _tokenService.GenerateJWTToken(claimsIdentity);

                AddUserClaims(claimsIdentity, userClaims, roles);

                var userDto = _mapper.Map<UserRequest>(user);

                userDto.Roles = roles;
                userDto.Token = token;

                return userDto.WithoutPassword();
            }

            return null;
        }

        private void AddUserClaims(ClaimsIdentity claimsIdentity, IList<Claim> userClaims, IList<string> roles)
        {
            foreach (var role in roles)
            {
                if (role.Equals(Roles.Administrador))
                {
                    foreach (var userClaim in userClaims)
                    {
                        claimsIdentity.AddClaim(new Claim(userClaim.Type, userClaim.Value));
                    }
                }

                if (role.Equals(Roles.Aplicador))
                {
                    foreach (var userClaim in userClaims)
                    {
                        claimsIdentity.AddClaim(new Claim(userClaim.Type, userClaim.Value));
                    }
                }
            }
        }
    }
}
