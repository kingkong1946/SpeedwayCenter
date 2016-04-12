using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpeedwayCenter.Areas.Admin.ViewModels;
using SpeedwayCenter.Areas.Admin.ViewModels.Match;
using SpeedwayCenter.Interface.Enums;
using SpeedwayCenter.ORM;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;

namespace SpeedwayCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class MatchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MatchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var matchesRepository = _unitOfWork.GetQueryRepository<TwoTeamMeeting>();
            var matches = matchesRepository.GetAll().ToList();
            var records = matches.Select(r => new AdminIndexMatchViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score
            });
            return View(records);
        }

        [HttpGet]
        public ActionResult AddStep1()
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var allTeams = GetTeamsList(teams);

            var viewModel = new AdminAddTeamsMatchViewModel
            {
                Teams = allTeams
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddStep2(AdminAddTeamsMatchViewModel item)
        {
            var riders = _unitOfWork.GetQueryRepository<Rider>();
            var teams = _unitOfWork.GetQueryRepository<Team>();

            var homeTeamRiders = riders.FindMany(
                r => r.Teams
                    .Any(t => t.Id == item.HomeTeamId))
                .ToList()
                .Select(r => new AdminBasicInfoViewModel
                {
                    Id = r.Id,
                    Name = $"{r.Name} {r.Forname}"
                })
                .ToList();

            homeTeamRiders.Insert(0, new AdminBasicInfoViewModel());

            var awayTeamRiders = riders.FindMany(
                r => r.Teams
                    .Any(t => t.Id == item.AwayTeamId))
                .ToList()
                .Select(r => new AdminBasicInfoViewModel
                {
                    Id = r.Id,
                    Name = $"{r.Name} {r.Forname}"
                })
                .ToList();

            awayTeamRiders.Insert(0, new AdminBasicInfoViewModel());

            var homeTeam = teams.FindBy(t => t.Id == item.HomeTeamId);
            var awayTeam = teams.FindBy(t => t.Id == item.AwayTeamId);

            var viewModel = new AdminAddRidersMatchViewModel
            {
                HomeTeamId = item.HomeTeamId,
                HomeTeam = homeTeam.FullName,
                AwayTeamId = item.AwayTeamId,
                AwayTeam = awayTeam.FullName,
                HomeTeamRiders = homeTeamRiders,
                AwayTeamRiders = awayTeamRiders
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddStep3(AdminAddRidersMatchViewModel item)
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var seasons = _unitOfWork.GetQueryRepository<Season>();
            var riders = _unitOfWork.GetQueryRepository<Rider>();
            var matches = _unitOfWork.GetRepository<TwoTeamMeeting>();

            var record = new TwoTeamMeeting();

            var heat1GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider1Id)
            };
            var heat1GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider9Id)
            };
            var heat1GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider2Id)
            };
            var heat1GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider10Id)
            };
            var heat2GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider15Id)
            };
            var heat2GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider6Id)
            };
            var heat2GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider14Id)
            };
            var heat2GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider7Id)
            };
            var heat3GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider11Id)
            };
            var heat3GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider3Id)
            };
            var heat3GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider12Id)
            };
            var heat3GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider4Id)
            };
            var heat4GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider5Id)
            };
            var heat4GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider15Id)
            };
            var heat4GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider7Id)
            };
            var heat4GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider13Id)
            };
            var heat5GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider12Id)
            };
            var heat5GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider1Id)
            };
            var heat5GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider11Id)
            };
            var heat5GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider2Id)
            };
            var heat6GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider3Id)
            };
            var heat6GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider13Id)
            };
            var heat6GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider4Id)
            };
            var heat6GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider14Id)
            };
            var heat7GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider9Id)
            };
            var heat7GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider5Id)
            };
            var heat7GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider10Id)
            };
            var heat7GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider6Id)
            };
            var heat8GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider13Id)
            };
            var heat8GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider2Id)
            };
            var heat8GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider15Id)
            };
            var heat8GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider1Id)
            };
            var heat9GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider4Id)
            };
            var heat9GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider10Id)
            };
            var heat9GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider3Id)
            };
            var heat9GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider9Id)
            };
            var heat10GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider7Id)
            };
            var heat10GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider11Id)
            };
            var heat10GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider5Id)
            };
            var heat10GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider12Id)
            };
            var heat11GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider10Id)
            };
            var heat11GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider2Id)
            };
            var heat11GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider13Id)
            };
            var heat11GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider3Id)
            };
            var heat12GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider6Id)
            };
            var heat12GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider14Id)
            };
            var heat12GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider1Id)
            };
            var heat12GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider11Id)
            };
            var heat13GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
                Rider = riders.FindBy(r => r.Id == item.Rider12Id)
            };
            var heat13GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
                Rider = riders.FindBy(r => r.Id == item.Rider4Id)
            };
            var heat13GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
                Rider = riders.FindBy(r => r.Id == item.Rider9Id)
            };
            var heat13GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
                Rider = riders.FindBy(r => r.Id == item.Rider5Id)
            };
            var heat14GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
            };
            var heat14GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
            };
            var heat14GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
            };
            var heat14GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
            };
            var heat15GateA = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.A,
            };
            var heat15GateB = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.B,
            };
            var heat15GateC = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.C,
            };
            var heat15GateD = new RiderResult
            {
                Id = Guid.NewGuid(),
                Gate = Gate.D,
            };

            var heat1Gates = new List<RiderResult>
            {
                heat1GateA,
                heat1GateB,
                heat1GateC,
                heat1GateD,
            };
            var heat2Gates = new List<RiderResult>
            {
                heat2GateA,
                heat2GateB,
                heat2GateC,
                heat2GateD,
            };
            var heat3Gates = new List<RiderResult>
            {
                heat3GateA,
                heat3GateB,
                heat3GateC,
                heat3GateD,
            };
            var heat4Gates = new List<RiderResult>
            {
                heat4GateA,
                heat4GateB,
                heat4GateC,
                heat4GateD,
            };
            var heat5Gates = new List<RiderResult>
            {
                heat5GateA,
                heat5GateB,
                heat5GateC,
                heat5GateD,
            };
            var heat6Gates = new List<RiderResult>
            {
                heat6GateA,
                heat6GateB,
                heat6GateC,
                heat6GateD,
            };
            var heat7Gates = new List<RiderResult>
            {
                heat7GateA,
                heat7GateB,
                heat7GateC,
                heat7GateD,
            };
            var heat8Gates = new List<RiderResult>
            {
                heat8GateA,
                heat8GateB,
                heat8GateC,
                heat8GateD,
            };
            var heat9Gates = new List<RiderResult>
            {
                heat9GateA,
                heat9GateB,
                heat9GateC,
                heat9GateD,
            };
            var heat10Gates = new List<RiderResult>
            {
                heat10GateA,
                heat10GateB,
                heat10GateC,
                heat10GateD,
            };
            var heat11Gates = new List<RiderResult>
            {
                heat11GateA,
                heat11GateB,
                heat11GateC,
                heat11GateD,
            };
            var heat12Gates = new List<RiderResult>
            {
                heat12GateA,
                heat12GateB,
                heat12GateC,
                heat12GateD,
            };
            var heat13Gates = new List<RiderResult>
            {
                heat13GateA,
                heat13GateB,
                heat13GateC,
                heat13GateD,
            };
            var heat14Gates = new List<RiderResult>
            {
                heat14GateA,
                heat14GateB,
                heat14GateC,
                heat14GateD,
            };
            var heat15Gates = new List<RiderResult>
            {
                heat15GateA,
                heat15GateB,
                heat15GateC,
                heat15GateD,
            };

            var heat1 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 1,
                Gates = heat1Gates
            };
            var heat2 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 2,
                Gates = heat2Gates
            };
            var heat3 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 3,
                Gates = heat3Gates
            };
            var heat4 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 4,
                Gates = heat4Gates
            };
            var heat5 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 5,
                Gates = heat5Gates
            };
            var heat6 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 6,
                Gates = heat6Gates
            };
            var heat7 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 7,
                Gates = heat7Gates
            };
            var heat8 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 8,
                Gates = heat8Gates
            };
            var heat9 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 9,
                Gates = heat9Gates
            };
            var heat10 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 10,
                Gates = heat10Gates
            };
            var heat11 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 11,
                Gates = heat11Gates
            };
            var heat12 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 12,
                Gates = heat12Gates
            };
            var heat13 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 13,
                Gates = heat13Gates
            };
            var heat14 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 14,
                Gates = heat14Gates
            };
            var heat15 = new Heat
            {
                Id = Guid.NewGuid(),
                Number = 15,
                Gates = heat15Gates
            };

            var heats = new List<Heat>
            {
                heat1,
                heat2,
                heat3,
                heat4,
                heat5,
                heat6,
                heat7,
                heat8,
                heat9,
                heat10,
                heat11,
                heat12,
                heat13,
                heat14,
                heat15
            };

            var homeTeamRiders = new List<Rider>
            {
                riders.FindBy(r => r.Id == item.Rider9Id),
                riders.FindBy(r => r.Id == item.Rider10Id),
                riders.FindBy(r => r.Id == item.Rider11Id),
                riders.FindBy(r => r.Id == item.Rider12Id),
                riders.FindBy(r => r.Id == item.Rider13Id),
                riders.FindBy(r => r.Id == item.Rider14Id),
                riders.FindBy(r => r.Id == item.Rider15Id)
            };

            var awayTeamRiders = new List<Rider>
            {
                riders.FindBy(r => r.Id == item.Rider1Id),
                riders.FindBy(r => r.Id == item.Rider2Id),
                riders.FindBy(r => r.Id == item.Rider3Id),
                riders.FindBy(r => r.Id == item.Rider4Id),
                riders.FindBy(r => r.Id == item.Rider5Id),
                riders.FindBy(r => r.Id == item.Rider6Id),
                riders.FindBy(r => r.Id == item.Rider7Id)
            };

            record = new TwoTeamMeeting
            {
                Id = Guid.NewGuid(),
                HomeTeam = teams.FindBy(t => t.Id == item.HomeTeamId),
                AwayTeam = teams.FindBy(t => t.Id == item.AwayTeamId),
                Season = seasons.FindBy(s => s.Name == "2016"),
                Status = Status.Finished,
                Name = $"{item.HomeTeam} - {item.AwayTeam}",
                Heats = heats,
                HomeTeamRiders = homeTeamRiders,
                AwayTeamRiders = awayTeamRiders,
                Riders = homeTeamRiders.Concat(awayTeamRiders).ToList()
            };

            matches.Add(record);
            var homeTeamRidersViewModel =
                record.HomeTeamRiders.Select(r => new AdminBasicInfoViewModel { Id = r.Id, Name = r.FullName }).ToList();

            var awayTeamRidersViewModel =
                record.AwayTeamRiders.Select(r => new AdminBasicInfoViewModel { Id = r.Id, Name = r.FullName }).ToList();

            homeTeamRidersViewModel.Insert(0, new AdminBasicInfoViewModel());
            awayTeamRidersViewModel.Insert(0, new AdminBasicInfoViewModel());

            var viewModel = new AdminAddScoresMatchViewModel
            {
                Id = record.Id,
                HomeTeamRiders = homeTeamRidersViewModel,
                AwayTeamRiders = awayTeamRidersViewModel,
                Heats = record.Heats.Select(x => new AdminHeatMatchViewModel
                {
                    Number = x.Number,
                    RiderIdGateA = x.Gates.FirstOrDefault(r => r.Gate == Gate.A).Rider?.Id ?? Guid.Empty,
                    RiderGateA = x.Gates.FirstOrDefault(r => r.Gate == Gate.A).Rider?.FullName ?? string.Empty,

                    RiderIdGateB = x.Gates.FirstOrDefault(r => r.Gate == Gate.B).Rider?.Id ?? Guid.Empty,
                    RiderGateB = x.Gates.FirstOrDefault(r => r.Gate == Gate.B).Rider?.FullName ?? string.Empty,

                    RiderIdGateC = x.Gates.FirstOrDefault(r => r.Gate == Gate.C).Rider?.Id ?? Guid.Empty,
                    RiderGateC = x.Gates.FirstOrDefault(r => r.Gate == Gate.C).Rider?.FullName ?? string.Empty,

                    RiderIdGateD = x.Gates.FirstOrDefault(r => r.Gate == Gate.D).Rider?.Id ?? Guid.Empty,
                    RiderGateD = x.Gates.FirstOrDefault(r => r.Gate == Gate.D).Rider?.FullName ?? string.Empty
                }).OrderBy(m => m.Number).ToList()
            };

            _unitOfWork.Save();

            var results = record.Heats.SelectMany(h => h.Gates);

            foreach (var riderResult in results)
            {
                riderResult.Meeting = record;
            }

            _unitOfWork.Save();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(AdminAddScoresMatchViewModel item)
        {
            var riders = _unitOfWork.GetQueryRepository<Rider>();
            var matches = _unitOfWork.GetRepository<TwoTeamMeeting>();
            var heats = _unitOfWork.GetRepository<Heat>();

            var record = matches.FindBy(m => m.Id == item.Id);

            record.Date = item.Date;
            record.Round = item.Round;

            var matchHeats = heats.FindMany(h => h.Meeting.Id == item.Id);

            foreach (var matchHeat in matchHeats)
            {
                var number = matchHeat.Number;

                var heat = item.Heats.FirstOrDefault(m => m.Number == number);

                var gateA = matchHeat.Gates.FirstOrDefault(r => r.Gate == Gate.A);
                gateA.Points = heat.RiderScoreGateA;
                gateA.Rider = riders.FindBy(r => r.Id == heat.RiderIdGateA);

                var gateB = matchHeat.Gates.FirstOrDefault(r => r.Gate == Gate.B);
                gateB.Points = heat.RiderScoreGateB;
                gateB.Rider = riders.FindBy(r => r.Id == heat.RiderIdGateB);

                var gateC = matchHeat.Gates.FirstOrDefault(r => r.Gate == Gate.C);
                gateC.Points = heat.RiderScoreGateC;
                gateC.Rider = riders.FindBy(r => r.Id == heat.RiderIdGateC);

                var gateD = matchHeat.Gates.FirstOrDefault(r => r.Gate == Gate.D);
                gateD.Points = heat.RiderScoreGateD;
                gateD.Rider = riders.FindBy(r => r.Id == heat.RiderIdGateD);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", "Match");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var match = _unitOfWork.GetRepository<TwoTeamMeeting>();
            var heats = _unitOfWork.GetRepository<Heat>();

            var thisHeats = heats.FindMany(h => h.Meeting.Id == id);
            foreach (var thisHeat in thisHeats)
            {
                heats.Delete(thisHeat);
            }
            var record = match.FindBy(r => r.Id == id);
            match.Delete(record);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    var riders = _unitOfWork.GetQueryRepository<Rider>();
        //    var teams = _unitOfWork.GetQueryRepository<Team>();
        //    var allTeams = GetTeamsList(teams);
        //    var rider = riders.FindBy(r => r.Id == id);
        //    var teamId = rider.Teams?
        //        .Where(t =>
        //            t.Seasons.Any(s =>
        //                s.Name == "2016" &&
        //                s.League.Name == "Speedway Ekstraliga"))
        //        .Select(t => t.Id)
        //        .FirstOrDefault() ?? Guid.Empty;
        //    var viewModel = new AdminEditRiderViewModel
        //    {
        //        Id = rider.Id,
        //        Name = rider.Name,
        //        Forname = rider.Forname,
        //        BirthDate = rider.BirthDate,
        //        Country = rider.Country,
        //        Teams = allTeams,
        //        TeamId = teamId
        //    };

        //    return View(viewModel);
        //}

        //[HttpPost]
        //public ActionResult Edit(AdminEditRiderViewModel item)
        //{
        //    var riders = _unitOfWork.GetRepository<Rider>();
        //    var teams = _unitOfWork.GetQueryRepository<Team>();

        //    var record = riders.FindBy(r => r.Id == item.Id);

        //    record.Id = item.Id;
        //    record.Name = item.Name;
        //    record.Forname = item.Forname;
        //    record.BirthDate = item.BirthDate;
        //    record.Country = item.Country;

        //    if (item.TeamId != Guid.Empty)
        //    {
        //        var team = teams.FindBy(t => t.Id == item.TeamId);
        //        record.Teams = new List<Team>
        //        {
        //            team
        //        };
        //    }
        //    else
        //    {
        //        record.Teams = null;
        //    }

        //    riders.Edit(record);
        //    riders.Save();

        //    return RedirectToAction("Index");
        //}

        private static IEnumerable<AdminBasicInfoViewModel> GetTeamsList(IQueryRepository<Team> teams)
        {
            var allTeams = teams.GetAll().ToList().Select(t => new AdminBasicInfoViewModel
            {
                Id = t.Id,
                Name = $"{t.Name} {t.City}"
            }).ToList();

            allTeams.Insert(0, new AdminBasicInfoViewModel
            {
                Id = Guid.Empty,
                Name = ""
            });

            return allTeams;
        }
    }
}