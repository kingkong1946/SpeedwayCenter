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

namespace SpeedwayCenter.Controllers
{
    public class TableController : Controller
    {
        private readonly IQueryRepository<Team> _teams;
        private readonly IQueryRepository<Season> _seasons;

        public TableController(IQueryRepository<Team> teams, IQueryRepository<Season> seasons)
        {
            _teams = teams;
            _seasons = seasons;
        }

        public ActionResult Index()
        {
            var thisSeason = _seasons.FindBy(s =>
                s.Name == "2016" &&
                s.League.Name == "Speedway Ekstraliga");

            var records = _teams
                .FindMany(team => team.Seasons.Any(season => season.Id == thisSeason.Id))
                .ToList();

            var viewModel = records.Select(x => new TableIndexViewModel(
                x.Name,
                x.GetMatchCountFromSeason(thisSeason),
                x.GetStatisticsFromSeason(thisSeason, i => i > 0 ? 1 : 0),
                x.GetStatisticsFromSeason(thisSeason, i => i == 0 ? 1 : 0),
                x.GetStatisticsFromSeason(thisSeason, i => i < 0 ? 1 : 0),
                x.GetStatisticsFromSeason(thisSeason, i =>
                {
                    if (i > 0) return 2;
                    if (i == 0) return 1;
                    return 0;
                }), x.GetPlusMinusPointsFromSeason(thisSeason)));

            return View(viewModel);
        }
    }
}