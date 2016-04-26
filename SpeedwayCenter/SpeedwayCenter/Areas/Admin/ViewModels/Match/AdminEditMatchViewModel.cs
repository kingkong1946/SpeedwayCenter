using System;
using System.Collections.Generic;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminEditMatchViewModel
    {
        public Guid Id { get; set; }

        public int Round { get; set; }
        public DateTime Date { get; set; }

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> HomeTeamsRiders { get; set; }
        public IEnumerable<AdminBasicInfoViewModel> AwayTeamsRiders { get; set; }

        public IList<HomeTeamRiders> HomeTeamSelectedRiders { get; set; }
        public IList<AwayTeamRiders> AwayTeamSelectedRiders { get; set; }

        public IList<AdminHeatMatchViewModel> Heats { get; set; }

        public readonly int[] HomeTeamACHeats = { 2, 3, 5, 7, 8, 11, 13, 15 };
    }
}