using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Ninject.Infrastructure.Language;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels;
using SpeedwayCenter.ViewModels.Rider;

namespace SpeedwayCenter.Controllers
{
    public class RiderController : Controller
    {
        private readonly IQueryRepository<Rider> _repository;
        private const int Take = 10;

        public RiderController(IQueryRepository<Rider> repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var records = _repository
                .GetAll()
                .Select(r => new
                {
                    r.Id,
                    r.Name,
                    r.Forname,
                    r.BirthDate,
                    r.Country
                }).ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                records = records
                    .Where(a => $"{a.Name} {a.Forname}".ToLower().Contains(searchValue.ToLower()))
                    .ToList();
            }

            var viewModel = records
                .Skip((page - 1) * Take)
                .Take(Take)
                .Select(r => new RiderIndexViewModel(
                    r.Id,
                    $"{r.Name} {r.Forname}",
                    r.BirthDate.ToShortDateString(),
                    r.Country))
                .ToEnumerable();

            ViewBag.NumberOfPages = (int)Math.Ceiling((decimal)records.Count / Take);
            ViewBag.Page = page;

            return View(viewModel);
        }

        public ViewResult Details(Guid id)
        {
            var rider = _repository.FindBy(r => r.Id == id);
            var team = rider.Teams.FirstOrDefault();

            BasicInfoViewModel viewModelTeam = null;

            if (team != null)
            {
                viewModelTeam = new BasicInfoViewModel
                {
                    Id = team.Id,
                    Name = team.FullName
                };
            }

            IList<RiderMatchViewModel> viewModelMatches = rider
                .HomeMeetings
                .Select(r => r.Match)
                .Concat(rider.AwayMeetings.Select(r => r.Match))
                .Select(m => new RiderMatchViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Date = m.Date?.ToShortDateString() ?? m.Status.ToString(),
                    Score = $"{m.HomeTeamPoints}:{m.AwayTeamPoints}",
                    Points = rider.GetPointsFromMeeting(m),
                    Total = rider.GetTotalPointsFromMeeting(m)
                }).ToList();

            var viewModel = new RiderDetailsViewModel
            {
                Name = rider.FullName,
                BirthDate = rider.BirthDate.ToShortDateString(),
                Country = rider.Country,
                Team = viewModelTeam,
                Matches = viewModelMatches
            };

            return View(viewModel);
        }
    }
}