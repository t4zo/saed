using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SAED.Api.Entities.Responses;
using SAED.Api.Interfaces;
using SAED.ApplicationCore.Interfaces;
using SAED.Infrastructure.Data;
using SAED.Infrastructure.Identity;
using static SAED.ApplicationCore.Constants.AuthorizationConstants;

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
            SignInResult result = await _signInManager.PasswordSignInAsync(username, password, remember, false);

            if (!result.Succeeded)
            {
                throw new AuthenticationException("Usuário e/ou Senha inválido(s)");
            }

            ApplicationUser user = await _userManager.FindByNameAsync(username);
            IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
            IList<string> roles = await _userManager.GetRolesAsync(user);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(Remember, remember.ToString().ToLower())
            });

            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            string token = _tokenService.GenerateJWTToken(claimsIdentity);

            AddUserClaims(claimsIdentity, userClaims, roles);

            user.LastLogin = DateTime.Now;
            await _context.SaveChangesAsync();

            UserResponse userResponse = _mapper.Map<UserResponse>(user);

            userResponse.Roles = roles;
            userResponse.Token = token;

            return userResponse;
        }

        private void AddUserClaims(ClaimsIdentity claimsIdentity, IList<Claim> userClaims, IList<string> roles)
        {
            foreach (string role in roles)
            {
                if (role.Equals(Roles.Administrador))
                {
                    foreach (Claim userClaim in userClaims)
                    {
                        claimsIdentity.AddClaim(new Claim(userClaim.Type, userClaim.Value));
                    }
                }

                if (role.Equals(Roles.Aplicador))
                {
                    foreach (Claim userClaim in userClaims)
                    {
                        claimsIdentity.AddClaim(new Claim(userClaim.Type, userClaim.Value));
                    }
                }
            }
        }
    }
}