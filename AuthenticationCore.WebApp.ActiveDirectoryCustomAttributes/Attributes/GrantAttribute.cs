using AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Providers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryCustomAttributes.Attributes
{
    /// <summary>
    /// Sample of a Grant Attribute in my WebApp to manage Reading, Writing and Deleting.
    /// </summary>
    internal class GrantAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public GrantAttribute(Grant grant) => Grant = grant;

        public Grant Grant
        {
            get
            {
                if (Enum.TryParse(Policy.Substring(MyProvider.GRANTPREFIXLENGTH), out Grant grant)) return grant;
                return Grant.Reader;
            }
            set
            {
                //Add Prefix to allow MyProvider to understand what attribute is (MyProvider.cs line 36)
                Policy = $"{MyProvider.GRANTPREFIX}{value}";
            }
        }
    }
    internal enum Grant
    {
        Reader,
        Writer,
        Deleter
    }
}
