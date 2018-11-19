using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes
{
    /// <summary>
    /// Sample of a Role Attribute in my WebApp to manage Administrator, Editor, some typologies of users
    /// </summary>
    public class RoleAttribute : AuthorizeAttribute
    {
        public new Role[] Roles { get; set; }
        public RoleAttribute(params Role[] role) : base("Role") => Roles = role;
    }
    public enum Role
    {
        Administrator,
        Editor,
        User
    }
}
