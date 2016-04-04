using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels.Team;

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
            var records = _queryRepository
                .GetAll()
                .ToList();

            var viewModel = records.Select(t => new TeamIndexViewModel(
                    $"{t.FullName}",
                    t.StadiumName,
                    t.Capacity));

            return View(viewModel);
        }
    }
}