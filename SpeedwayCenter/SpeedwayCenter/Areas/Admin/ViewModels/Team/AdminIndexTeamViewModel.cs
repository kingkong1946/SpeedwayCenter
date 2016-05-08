using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Team
{
    public class AdminIndexTeamViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Stadium Name")]
        public string StadiumName { get; set; }
        public int Capacity { get; set; }
    }
}