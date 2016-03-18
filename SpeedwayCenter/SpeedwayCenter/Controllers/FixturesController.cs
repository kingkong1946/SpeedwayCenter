using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Models.Models;
using SpeedwayCenter.Models.Repository;
using SpeedwayCenter.ViewModels;
using SpeedwayCenter.ViewModels.Meeting;

namespace SpeedwayCenter.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IQueryRepository<Meeting> _repository;

        public FixturesController(IQueryRepository<Meeting> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var allMeetings = _repository
                .GetAll()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToList();

            var meetings = allMeetings.Select(m => new MeetingFixtureIndexViewModel(
                    m.Date.ToString("f"),
                    $"{m.HomeTeam.Name} {m.HomeTeam.City} - {m.AwayTeam.Name} {m.AwayTeam.City}",
                    null))
                .ToList();

            var count = _repository
                .GetAll()
                .Distinct()
                .Count();

            return View(new FixturesViewModel(count, meetings));
        }
    }
}