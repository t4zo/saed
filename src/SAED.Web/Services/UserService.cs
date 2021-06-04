using Microsoft.AspNetCore.Identity;
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