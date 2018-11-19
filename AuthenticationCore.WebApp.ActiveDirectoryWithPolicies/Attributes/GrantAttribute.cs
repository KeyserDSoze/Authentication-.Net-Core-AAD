using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes
{
    /// <summary>
    /// Sample of a Grant Attribute in my WebApp to manage Reading, Writing and Deleting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class GrantAttribute : AuthorizeAttribute
    {
        public Grant Grant { get; set; }
        public GrantAttribute(Grant grant) : base("Grant") => Grant = grant;
    }
    public enum Grant
    {
        Reader,
        Writer,
        Deleter
    }
}
