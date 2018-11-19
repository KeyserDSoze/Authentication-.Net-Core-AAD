using AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Handlers
{
    public class GrantRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
    }
    /// <summary>
    /// Concretion of AttributeAuthorizationHandler to link GrantRequirment and GrantAttribute. See Startup.cs line 42 and line 47
    /// </summary>
    public class GrantHandler : AttributeAuthorizationHandler<GrantRequirement, GrantAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GrantRequirement requirement, IEnumerable<GrantAttribute> attributes)
        {
            //check all possible attributes if you set in GrantAttribute "AllowMultiple = true"
            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(context.User, permissionAttribute.Grant.ToString())) return;
            }
            context.Succeed(requirement);
        }
    }
}
