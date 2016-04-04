using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Team
{
    public class AdminEditTeamViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Display(Name = "City: ")]
        public string City { get; set; }

        [Display(Name = "Stadium Name: ")]
        public string StadiumName { get; set; }

        [Display(Name = "Capacity: ")]
        public int Capacity { get; set; }

        [Display(Name = "Riders: ")]
        public IEnumerable<AdminEditRidersTeamViewModel> Riders { get; set; }
    }
}