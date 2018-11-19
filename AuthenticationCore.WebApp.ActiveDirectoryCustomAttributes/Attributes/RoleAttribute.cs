using AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Providers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Attributes
{
    /// <summary>
    /// Sample of a Role Attribute in my WebApp to manage Administrator, Editor, some typologies of users
    /// </summary>
    internal class RoleAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public RoleAttribute(params Role[] role) => Role = role;

        public Role[] Role
        {
            get => Policy.Substring(MyProvider.ROLEPREFIXLENGTH).Split(',').Select(x => (Role)Enum.Parse(typeof(Role), x)).ToArray();
            set
            {
                //Add Prefix to allow MyProvider to understand what attribute is (MyProvider.cs line 30)
                Policy = $"{MyProvider.ROLEPREFIX}{string.Join(",", value)}";
            }
        }
    }
    internal enum Role
    {
        Administrator,
        Editor,
        User
    }
}
