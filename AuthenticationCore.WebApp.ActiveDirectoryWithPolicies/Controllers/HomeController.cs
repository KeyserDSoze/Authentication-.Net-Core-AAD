using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Models;
using AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes;
using static AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes.Grant;
using static AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Attributes.Role;

namespace AuthenticationCore.WebApp.ActiveDirectoryWithPolicies.Controllers
{
    //Using of Role
    [Role(Administrator, Editor)]
    public class HomeController : Controller
    {
        //Using of Grant
        [Grant(Reader)]
        public IActionResult Index()
        {
            return View();
        }

        //Using of Grant
        [Grant(Writer)]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
