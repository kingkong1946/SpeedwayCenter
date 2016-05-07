using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Team
{
    public class TeamDetailsViewModel
    {
        public string Name { get; set; }

        [Display(Name = "Stadium Name")]
        public string StadiumName { get; set; }

        public int Capacity { get; set; }

        public IEnumerable<BasicInfoViewModel> Riders { get; set; }
        public IEnumerable<TeamMatchViewModel> Matches { get; set; }
    }
}