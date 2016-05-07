using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels;
using SpeedwayCenter.ViewModels.Table;
using SpeedwayCenter.ViewModels.Team;

namespace SpeedwayCenter.Controllers
{
    public class TeamController : Controller
    {
        private readonly IQueryRepository<Team> _queryRepository;
        private const int Take = 10;

        public TeamController(IQueryRepository<Team> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var records = _queryRepository
                .GetAll()
                .ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                records = records
                    .Where(a => $"{a.Name} {a.City}".ToLower().Contains(searchValue.ToLower()))
                    .ToList();
            }

            var viewModel = records
                .Skip((page - 1) * Take)
                .Take(Take)
                .Select(t => new TeamIndexViewModel(
                    t.Id,
                    $"{t.FullName}",
                    t.StadiumName,
                    t.Capacity));

            ViewBag.NumberOfPages = (int)Math.Ceiling((decimal)records.Count / Take);
            ViewBag.Page = page;

            return View(viewModel);
        }

        public ViewResult Details(Guid id)
        {
            var team = _queryRepository.FindBy(t => t.Id == id);

            var riders = team.Riders.Select(r => new BasicInfoViewModel
            {
                Id = r.Id,
                Name = r.FullName
            });

            var matches = team.AllMeetings.Select(m => new TeamMatchViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Score = $"{m.HomeTeamPoints}:{m.AwayTeamPoints}",
                Date = m.Date?.ToShortDateString() ?? m.Status.ToString()
            });

            var viewModel = new TeamDetailsViewModel
            {
                Name = team.Name,
                StadiumName = team.StadiumName,
                Capacity = team.Capacity,
                Riders = riders,
                Matches = matches
            };

            return View(viewModel);
        }
    }
}