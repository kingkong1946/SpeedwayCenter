using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpeedwayCenter.Areas.Admin.ViewModels;
using SpeedwayCenter.Areas.Admin.ViewModels.Rider;
using SpeedwayCenter.ORM;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;

namespace SpeedwayCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class RiderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RiderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var riders = _unitOfWork.GetQueryRepository<Rider>();
            var records = riders.GetAll().ToList().Select(r => new AdminIndexRiderViewModel
            {
                Id = r.Id,
                Name = r.FullName,
                BirthDate = r.BirthDate,
                Country = r.Country
            });
            return View(records);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var allTeams = GetTeamsList(teams);

            var viewModel = new AdminAddRiderViewModel
            {
                Teams = allTeams
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(AdminAddRiderViewModel item)
        {
            var riders = _unitOfWork.GetRepository<Rider>();
            var teams = _unitOfWork.GetQueryRepository<Team>();

            var record = new Rider
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Forname = item.Forname,
                BirthDate = item.BirthDate,
                Country = item.Country
            };

            if (item.TeamId != Guid.Empty)
            {
                var team = teams.FindBy(t => t.Id == item.TeamId);
                record.Teams = new List<Team>
                {
                    team
                };
            }

            riders.Add(record);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            var riders = _unitOfWork.GetRepository<Rider>();
            var record = riders.FindBy(r => r.Id == id);
            riders.Delete(record);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var riders = _unitOfWork.GetQueryRepository<Rider>();
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var allTeams = GetTeamsList(teams);
            var rider = riders.FindBy(r => r.Id == id);
            var teamId = rider.Teams?
                //.Where(t =>
                //    t.Seasons.Any(s =>
                //        s.Name == "2016" &&
                //        s.League.Name == "Speedway Ekstraliga"))
                .Select(t => t.Id)
                .FirstOrDefault() ?? Guid.Empty;
            var viewModel = new AdminEditRiderViewModel
            {
                Id = rider.Id,
                Name = rider.Name,
                Forname = rider.Forname,
                BirthDate = rider.BirthDate,
                Country = rider.Country,
                Teams = allTeams,
                TeamId = teamId
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AdminEditRiderViewModel item)
        {
            var riders = _unitOfWork.GetRepository<Rider>();
            var teams = _unitOfWork.GetQueryRepository<Team>();

            var record = riders.FindBy(r => r.Id == item.Id);

            record.Id = item.Id;
            record.Name = item.Name;
            record.Forname = item.Forname;
            record.BirthDate = item.BirthDate;
            record.Country = item.Country;

            if (item.TeamId != Guid.Empty)
            {
                var team = teams.FindBy(t => t.Id == item.TeamId);
                record.Teams = new List<Team>
                {
                    team
                };
            }
            else
            {
                record.Teams = null;
            }

            riders.Edit(record);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        private static IEnumerable<AdminBasicInfoViewModel> GetTeamsList(IQueryRepository<Team> teams)
        {
            var allTeams = teams.GetAll().Select(t => new AdminBasicInfoViewModel
            {
                Id = t.Id,
                Name = t.Name
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