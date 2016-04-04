using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using SpeedwayCenter.Infrastructure;
using SpeedwayCenter.ViewModels;

namespace SpeedwayCenter.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationProvider _authenticationProvider;

        public LoginController(IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Rider", new { area = "Admin" });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(login.UserName) ||
                    string.IsNullOrEmpty(login.Password))
                {
                    ModelState.AddModelError("NotFound", "Username or password is empty");
                    return View();
                }
                if (_authenticationProvider.Authenticate(login.UserName, login.Password))
                {
                    return Redirect(Url.Action("Index", "Rider", new { area = "Admin" }));
                }

                ModelState.AddModelError("NotFound", "Invalid username or password");
            }
            return View();
        }
    }
}