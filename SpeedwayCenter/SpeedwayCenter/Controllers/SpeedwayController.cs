using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedwayCenter.Controllers
{
    public class SpeedwayController : Controller
    {
        private DbContext context;
        public ActionResult Index()
        {
            return View();
        }
    }
}