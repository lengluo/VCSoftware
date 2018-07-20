using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VCSoftware.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using VCSoftware.Demo.Web.Models;

namespace VCSoftware.Demo.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public IActionResult Index()
        {
            base.SignIn("vitchen", "666", () => "vid");
            var t = CurrentUser;
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public IActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LogOn(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewData["Message"] = "Please enter a username and the password!";
            }
            return Json(base.SignIn(userName, password, () => "vid"));
        }
    }
}
