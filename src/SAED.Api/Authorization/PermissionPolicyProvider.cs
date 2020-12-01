﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using static SAED.Core.Constants.AuthorizationConstants;

namespace SAED.Api.Authorization
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // Só pode haver um provedor de políticas no ASP.NET Core.
            // Apenas lidamos com políticas relacionadas a permissões, para o resto, usaremos o provedor padrão.
            _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        private DefaultAuthorizationPolicyProvider _fallbackPolicyProvider { get; }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _fallbackPolicyProvider.GetDefaultPolicyAsync();
        }


        // Cria dinamicamente uma política com um requisito que contém a permissão.
        // O nome da política deve corresponder à permissão necessária.
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(CustomClaimTypes.Permissions, StringComparison.OrdinalIgnoreCase))
            {
                AuthorizationPolicyBuilder policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }

            // Policy is not for permissions, try the default provider.
            return _fallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return _fallbackPolicyProvider.GetFallbackPolicyAsync();
        }
    }
}