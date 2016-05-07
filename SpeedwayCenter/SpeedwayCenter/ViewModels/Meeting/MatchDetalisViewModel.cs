using System.Collections.Generic;

namespace SpeedwayCenter.ViewModels.Meeting
{
    public class MatchDetalisViewModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public int HomeTeamPoints { get; set; }
        public int AwayTeamPoints { get; set; }

        public IList<MatchRiderViewModel> HomeTeamRiders { get; set; }
        public IList<MatchRiderViewModel> AwayTeamRiders { get; set; }

        public IList<MatchHeatViewModel> Heats { get; set; }

        public int[] HomeACGates { get; } = { 2, 3, 5, 7, 8, 11, 13, 15 };
    }
}