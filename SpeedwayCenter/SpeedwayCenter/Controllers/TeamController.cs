using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;

namespace SpeedwayCenter.Controllers
{
    public class TeamController : Controller
    {
        private IRepository<Team> _repository;

        public TeamController(IRepository<Team> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}