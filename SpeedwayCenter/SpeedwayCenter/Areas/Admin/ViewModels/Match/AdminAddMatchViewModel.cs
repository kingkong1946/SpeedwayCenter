using System;
using System.Collections.Generic;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminAddTeamsMatchViewModel
    {
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> Teams { get; set; }
    }
}