using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SAED.Api.Entities.Responses;
using SAED.Api.Interfaces;
using SAED.Core.Interfaces;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserResponse> AuthenticateAsync(string username, string password, bool remember = false)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, remember, false);

            if (!result.Succeeded)
            {
                throw new AuthenticationException("Usuário e/ou Senha inválido(s)");
            }

            var user = await _userManager.FindByNameAsync(username);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(Remember, remember.ToString().ToLower())
            });

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var token = _tokenService.GenerateJWTToken(claimsIdentity);

            AddUserClaims(claimsIdentity, userClaims, roles);

            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            var userResponse = _mapper.Map<UserResponse>(user);

            userResponse.Roles = roles;
            userResponse.Token = token;

            return userResponse;
        }

        private void AddUserClaims(ClaimsIdentity claimsIdentity, IEnumerable<Claim> userClaims, IEnumerable<string> roles)
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