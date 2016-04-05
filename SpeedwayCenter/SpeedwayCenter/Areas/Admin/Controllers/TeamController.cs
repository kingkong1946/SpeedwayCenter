using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SpeedwayCenter.Areas.Admin.ViewModels.Rider;
using SpeedwayCenter.Areas.Admin.ViewModels.Team;
using SpeedwayCenter.ORM;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.Areas.Admin.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var records = teams.GetAll().Select(r => new AdminIndexTeamViewModel
            {
                Id = r.Id,
                Name = r.Name,
                City = r.City,
                StadiumName = r.StadiumName,
                Capacity = r.Capacity
            });
            return View(records);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AdminAddTeamViewModel item)
        {
            var teams = _unitOfWork.GetRepository<Team>();
            var seasons = _unitOfWork.GetQueryRepository<Season>();
            var thisSeason = seasons.FindBy(s => s.Name == "2016");

            var record = new Team
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                City = item.City,
                StadiumName = item.StadiumName,
                Capacity = item.Capacity,
                Seasons = new List<Season> { thisSeason }
            };

            teams.Add(record);
            teams.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var teams = _unitOfWork.GetRepository<Team>();
            var record = teams.FindBy(r => r.Id == id);
            teams.Delete(record);
            teams.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var teams = _unitOfWork.GetQueryRepository<Team>();
            var records = teams.FindBy(r => r.Id == id);
            var viewModel = new AdminEditTeamViewModel
            {
                Id = records.Id,
                Name = records.Name,
                City = records.City,
                StadiumName = records.StadiumName,
                Capacity = records.Capacity,
                Riders = records.Riders?.Select(r => new AdminEditRidersTeamViewModel
                {
                    Id = r.Id,
                    Name = $"{r.Name} {r.Forname}"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AdminEditTeamViewModel item)
        {
            var teams = _unitOfWork.GetRepository<Team>();
            var record = teams.FindBy(t => t.Id == item.Id);

            if (record == null)
            {
                record = new Team();
            }

            record.Id = item.Id;
            record.Name = item.Name;
            record.City = item.City;
            record.StadiumName = item.StadiumName;
            record.Capacity = item.Capacity;

            teams.Edit(record);
            teams.Save();

            return RedirectToAction("Index");
        }
    }
}