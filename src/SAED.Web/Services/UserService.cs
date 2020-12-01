﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SAED.Infrastructure.Identity;

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
            List<IdentityError> passwordErrors = new List<IdentityError>();
            foreach (IPasswordValidator<ApplicationUser> passwordValidator in _userManager.PasswordValidators)
            {
                IdentityResult validationResult = await passwordValidator.ValidateAsync(_userManager, null, password);
                if (!validationResult.Succeeded)
                {
                    passwordErrors = validationResult.Errors.ToList();
                }
            }

            return passwordErrors;
        }
    }
}