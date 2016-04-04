using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels;
using SpeedwayCenter.ViewModels.Fixture;
using SpeedwayCenter.ViewModels.Meeting;

namespace SpeedwayCenter.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IQueryRepository<TwoTeamMeeting> _repository;

        public FixturesController(IQueryRepository<TwoTeamMeeting> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var allMeetings = _repository
                .GetAll()
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Select(m => new
                {
                    m.Date,
                    m.HomeTeam,
                    m.AwayTeam,
                    m.Round
                })
                .ToList();

            var meetings = allMeetings.Select(m => new MeetingFixtureIndexViewModel(
                    m.Date.ToString("f"),
                    $"{m.HomeTeam.FullName} - {m.AwayTeam.FullName}",
                    null))
                .ToList();

            var count = allMeetings
                .Select(m => m.Round)
                .Distinct()
                .Count();

            return View(new FixtureViewModel(count, meetings));
        }
    }
}