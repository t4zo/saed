using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAED.Core.Constants;
using SAED.Core.Entities;
using SAED.Persistence.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAED.Web.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(string username, string email, string cpf)
        {
            var user = new ApplicationUser { UserName = username, Email = email };
            var result = await _userManager.CreateAsync(user, cpf);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AuthorizationConstants.Roles.Aluno);
            }
            
            return result;
        }
        
        public async Task<IdentityResult> UpdateUserAsync(Aluno novoAluno, Aluno alunoOriginal)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == alunoOriginal.Cpf.Normalize());

            await _userManager.SetEmailAsync(user, novoAluno.Cpf.Normalize());
            await _userManager.SetUserNameAsync(user, novoAluno.Cpf.Normalize());
            await _userManager.ChangePasswordAsync(user, alunoOriginal.Cpf.Normalize(), novoAluno.Cpf.Normalize());
            // user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, aluno.Cpf.Normalize());

            var result = await _userManager.UpdateAsync(user);

            return result;
        }

        public async Task<IEnumerable<IdentityError>> ValidatePasswordAsync(string password)
        {
            var passwordErrors = new List<IdentityError>();
            foreach (var passwordValidator in _userManager.PasswordValidators)
            {
                var validationResult = await passwordValidator.ValidateAsync(_userManager, null, password);
                if (!validationResult.Succeeded)
                {
                    passwordErrors = validationResult.Errors.ToList();
                }
            }

            return passwordErrors;
        }
    }
}