using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminAddTeamsMatchViewModel
    {
        [Display(Name = "Home Team")]
        public Guid HomeTeamId { get; set; }
        
        [Display(Name = "Away Team")]
        public Guid AwayTeamId { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> Teams { get; set; }
    }
}