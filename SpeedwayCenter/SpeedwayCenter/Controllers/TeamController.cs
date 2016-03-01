using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Models.Repository;

namespace SpeedwayCenter.Controllers
{
    public class TeamController : Controller
    {
        private IRepository<Team> 
        public ActionResult Index()
        {
            return View();
        }
    }
}