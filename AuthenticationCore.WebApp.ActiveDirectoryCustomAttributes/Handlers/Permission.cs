using AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Code
{
    /// <summary>
    /// Manage every authorization attribute I used in this webapp.
    /// </summary>
    public class PermissionHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements.ToList();

            //Pending requirements are all authorization attributes in Controller and Action called.
            foreach (var requirement in pendingRequirements)
            {
                if (requirement is RoleAttribute)
                {
                    foreach(Role role in ((RoleAttribute)requirement).Role)
                    {
                        if (IsInGroup(role.ToString())) context.Succeed(requirement);
                    }
                }
                else if (requirement is GrantAttribute)
                {
                    if (IsInGroup(((GrantAttribute)requirement).Grant.ToString()))
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;
        }

        private static bool IsInGroup(string GroupName)
        {
            var groups = new List<string>();
            //Get current Active Directory user
            var wi = WindowsIdentity.GetCurrent();
            if (wi.Groups != null)
            {
                //Check in Active Directory groups
                foreach (var group in wi.Groups)
                {
                    try
                    {
                        groups.Add(group.Translate(typeof(NTAccount)).ToString());
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                //sample of a translated name domain.com\groupname
                return groups.Contains(GroupName);
            }
            return false;
        }
    }
}
