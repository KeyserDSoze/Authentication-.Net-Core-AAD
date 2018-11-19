using AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Handlers
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        //Add any custom requirement properties if you have them
    }
    public class RoleHandler : AttributeAuthorizationHandler<RoleRequirement, RoleAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement, IEnumerable<RoleAttribute> attributes)
        {
            //check all possible attributes if you set in RoleAttribute "AllowMultiple = true"
            foreach (var permissionAttribute in attributes)
            {
                //logic OR for roles of my attribute
                foreach(Role role in permissionAttribute.Roles)
                {
                    if (await AuthorizeAsync(context.User, role.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return;
        }
    }
}
