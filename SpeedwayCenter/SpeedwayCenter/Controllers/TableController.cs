using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpeedwayCenter.Models.FluentApi;
using SpeedwayCenter.Models.Models;
using SpeedwayCenter.Models.Repository;
using SpeedwayCenter.ViewModels;

namespace SpeedwayCenter.Controllers
{
    public class TableController : Controller
    {
        private readonly IQueryRepository<Team> _queryRepository;

        public TableController(IQueryRepository<Team> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public ActionResult Index()
        {
            //var records = _queryRepository.GetAll()
            //    .Select(e => new TeamForTableViewModel(
            //        e.Name,
            //        e.AwayMeetings.Sum() + e.HomeMeetings.Sum(),
            //        ));
            return View(new List<Team>());
        }
    }
}