using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminAddScoresMatchViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Round { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> HomeTeamRiders { get; set; }
        public IEnumerable<AdminBasicInfoViewModel> AwayTeamRiders { get; set; }

        public IList<AdminHeatMatchViewModel> Heats { get; set; }

        public readonly int[] HomeTeamACHeats = { 2, 3, 5, 7, 8, 11, 13, 15 };
    }
}