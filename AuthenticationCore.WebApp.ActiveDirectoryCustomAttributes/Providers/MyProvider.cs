using AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Attributes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Providers
{
    internal class MyProvider : IAuthorizationPolicyProvider
    {
        internal const string ROLEPREFIX = "Role";
        internal static int ROLEPREFIXLENGTH => ROLEPREFIX.Length;
        internal const string GRANTPREFIX = "Grant";
        internal static int GRANTPREFIXLENGTH => GRANTPREFIX.Length;
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new NotImplementedException();
        }

        // Policies are looked up by string name, so expect 'parameters' (like Role)
        // to be embedded in the policy names. This is abstracted away from developers
        // by the more strongly-typed attributes derived from AuthorizeAttribute
        // (like [Role()] in this sample)
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            //This method prepares policy during every request to an Action.
            //Policy is created by policyName (using your string convention to divide every attribute you used)
            //In this sample I used ROLEPREFIX (RoleAttribute.cs line 23) and GRANTPREFIX (GrantAttribute.cs line 23) to understood policy.
            if (policyName.StartsWith(ROLEPREFIX, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new RoleAttribute(policyName.Substring(ROLEPREFIXLENGTH).Split(',').Select(x => (Role)Enum.Parse(typeof(Role), x)).ToArray()));
                return Task.FromResult(policy.Build());
            }
            else if (policyName.StartsWith(GRANTPREFIX, StringComparison.OrdinalIgnoreCase) &&
              Enum.TryParse(policyName.Substring(GRANTPREFIXLENGTH), out Grant grant))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new GrantAttribute(grant));
                return Task.FromResult(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
