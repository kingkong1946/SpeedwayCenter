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

        public IList<Heat> Heats { get; set; } 

        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public IList<AdminRiderForTeamViewModel> HomeTeamRiders { get; set; }
        public IList<AdminRiderForTeamViewModel> AwayTeamRiders { get; set; }
    }
}