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
using SpeedwayCenter.ViewModels.Meeting;

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
            var matches = _unitOfWork.GetQueryRepository<TwoTeamMeeting>();
            var records = matches.GetAll().ToList().Select(r => new AdminIndexMatchViewModel
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score
            });
            return View(records);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var allTeams = GetTeamsList(teams);

            var viewModel = new AdminAddTeamsMatchViewModel
            {
                Teams = allTeams
            };
            return View("AddStep1", viewModel);
        }

        [HttpPost]
        public ActionResult Add(AdminAddTeamsMatchViewModel item)
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
                    });

            var awayTeamRiders = riders.FindMany(
                r => r.Teams
                    .Any(t => t.Id == item.AwayTeamId))
                    .ToList()
                    .Select(r => new AdminBasicInfoViewModel
                    {
                        Id = r.Id,
                        Name = $"{r.Name} {r.Forname}"
                    });

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

            return View("AddStep2", viewModel);
        }

        [HttpPost]
        public ActionResult Add(AdminAddRidersMatchViewModel item)
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var seasons = _unitOfWork.GetQueryRepository<Season>();
            var riders = _unitOfWork.GetQueryRepository<Rider>();

            var record = new TwoTeamMeeting
            {
                HomeTeam = teams.FindBy(t => t.Id == item.HomeTeamId),
                AwayTeam = teams.FindBy(t => t.Id == item.AwayTeamId),
                Season = seasons.FindBy(s => s.Name == "2016"),
                Status = Status.Finished,
                Name = $"{item.HomeTeam} - {item.AwayTeam}",
                Heats = new List<Heat>
                {
                    new Heat
                    {
                        Id = Guid.NewGuid(),
                        Number = 1,
                        Gates = new List<RiderResult>
                        {
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.A,
                                Rider = riders.FindBy(r => r.Id == item.Rider1Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.B,
                                Rider = riders.FindBy(r => r.Id == item.Rider9Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.C,
                                Rider = riders.FindBy(r => r.Id == item.Rider2Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.D,
                                Rider = riders.FindBy(r => r.Id == item.Rider10Id)
                            }
                        }
                    },
                    new Heat
                    {
                        Id = Guid.NewGuid(),
                        Number = 2,
                        Gates = new List<RiderResult>
                        {
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.A,
                                Rider = riders.FindBy(r => r.Id == item.Rider15Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.B,
                                Rider = riders.FindBy(r => r.Id == item.Rider6Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.C,
                                Rider = riders.FindBy(r => r.Id == item.Rider14Id)
                            },
                            new RiderResult
                            {
                                Id = Guid.NewGuid(),
                                Gate = Gate.D,
                                Rider = riders.FindBy(r => r.Id == item.Rider7Id)
                            }
                        }
                    }
                }
            };

            //heats.Add(
        }

        //[HttpPost]
        //public ActionResult Delete(Guid id)
        //{
        //    var riders = _unitOfWork.GetRepository<Rider>();
        //    var record = riders.FindBy(r => r.Id == id);
        //    riders.Delete(record);
        //    riders.Save();

        //    return RedirectToAction("Index");
        //}

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