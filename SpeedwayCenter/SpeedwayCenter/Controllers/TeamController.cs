using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Models.Models;
using SpeedwayCenter.Models.Repository;
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
                .Take(10)
                .ToList();

            var viewModel = records.Select(t => new TeamIndexViewModel(
                    $"{t.Name} {t.City}",
                    t.StadiumName,
                    t.Capacity));

            return View(viewModel);
        }
    }
}