using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Interface.Enums;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels;
using SpeedwayCenter.ViewModels.Fixture;
using SpeedwayCenter.ViewModels.Meeting;
using SpeedwayCenter.ViewModels.Rider;

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
                .ToList()
                .Select(m => new
                {
                    m.Id,
                    m.Date,
                    m.HomeTeam,
                    m.AwayTeam,
                    m.Round,
                    m.Score
                });

            var meetings = allMeetings.Select(m => new MeetingFixtureIndexViewModel(
                    m.Id,
                    m.Date?.ToString("D"),
                    $"{m.HomeTeam.FullName} - {m.AwayTeam.FullName}",
                    m.Score,
                    m.Round))
                .ToList();

            var count = allMeetings
                .Select(m => m.Round)
                .Distinct()
                .OrderBy(i => i);

            return View(new FixtureViewModel(count, meetings));
        }


        public ViewResult Details(Guid id)
        {
            var match = _repository.FindBy(m => m.Id == id);

            var heats = match.Heats.Select(h => new MatchHeatViewModel
            {
                Number = h.Number,
                GateA = new HeatRiderViewModel
                {
                    Name = h.Gates.FirstOrDefault(r => r.Gate == Gate.A).Rider.FullName,
                    Score = h.Gates.FirstOrDefault(r => r.Gate == Gate.A).Points
                },
                GateB = new HeatRiderViewModel
                {
                    Name = h.Gates.FirstOrDefault(r => r.Gate == Gate.B).Rider.FullName,
                    Score = h.Gates.FirstOrDefault(r => r.Gate == Gate.B).Points
                },
                GateC = new HeatRiderViewModel
                {
                    Name = h.Gates.FirstOrDefault(r => r.Gate == Gate.C).Rider.FullName,
                    Score = h.Gates.FirstOrDefault(r => r.Gate == Gate.C).Points
                },
                GateD = new HeatRiderViewModel
                {
                    Name = h.Gates.FirstOrDefault(r => r.Gate == Gate.D).Rider.FullName,
                    Score = h.Gates.FirstOrDefault(r => r.Gate == Gate.D).Points
                }
            }).OrderBy(m => m.Number).ToList();

            var viewModel = new MatchDetalisViewModel
            {
                HomeTeam = match.HomeTeam.FullName,
                AwayTeam = match.AwayTeam.FullName,
                HomeTeamPoints = match.HomeTeamPoints,
                AwayTeamPoints = match.AwayTeamPoints,
                Heats = heats,
                HomeTeamRiders = match.HomeTeamRiders.Select(r => new MatchRiderViewModel
                {
                    Name = r.Rider.FullName,
                    Score = r.Rider.GetPointsFromMeeting(match),
                    Total = r.Rider.GetTotalPointsFromMeeting(match),
                    Number = r.Number
                }).OrderBy(m => m.Number).ToList(),
                AwayTeamRiders = match.AwayTeamRiders.Select(r => new MatchRiderViewModel
                {
                    Name = r.Rider.FullName,
                    Score = r.Rider.GetPointsFromMeeting(match),
                    Total = r.Rider.GetTotalPointsFromMeeting(match),
                    Number = r.Number
                }).OrderBy(m => m.Number).ToList(),
            };

            return View(viewModel);
        }
    }
}