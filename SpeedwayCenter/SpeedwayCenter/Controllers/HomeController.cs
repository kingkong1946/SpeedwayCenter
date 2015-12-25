using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedwayCenter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Page = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Page = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Page = "Contact";
            return View();
        }
    }
}