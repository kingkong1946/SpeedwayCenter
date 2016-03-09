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
        private readonly IQueryRepository<Team> _queryRepository;

        public TeamController(IQueryRepository<Team> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public ActionResult Index()
        {
            var records = _queryRepository.GetAll().Take(10);
            return View(records);
        }
    }
}