using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Handlers
{
    /// <summary>
    /// Abstraction that represents an AuthorizationHandler. It's created to have one or more handlers at same time.
    /// </summary>
    /// <typeparam name="TRequirement">Custom Requirement</typeparam>
    /// <typeparam name="TAttribute">Custom Attribute</typeparam>
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute> : AuthorizationHandler<TRequirement> where TRequirement : IAuthorizationRequirement where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            //Get attributes of controller and action
            var attributes = new List<TAttribute>();
            //Check if there's any authorizative attribute
            var action = (context.Resource as AuthorizationFilterContext)?.ActionDescriptor as ControllerActionDescriptor;
            if (action != null)
            {
                attributes.AddRange(GetAttributes(action.ControllerTypeInfo.UnderlyingSystemType));
                attributes.AddRange(GetAttributes(action.MethodInfo));
            }
            //Call Abstract method implemented by concretion one.
            return HandleRequirementAsync(context, requirement, attributes);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, IEnumerable<TAttribute> attributes);

        private static IEnumerable<TAttribute> GetAttributes(MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
        }
        private protected async Task<bool> AuthorizeAsync(ClaimsPrincipal user, string permission)
        {
            //Implement your custom user permission logic here
            await Task.Delay(0);
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
                return groups.Contains(permission);
            }
            return false;
        }
    }
}
