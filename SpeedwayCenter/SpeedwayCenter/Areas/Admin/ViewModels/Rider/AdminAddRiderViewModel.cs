using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Rider
{
    public class AdminAddRiderViewModel
    {
        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Display(Name = "Forname: ")]
        public string Forname { get; set; }

        [Display(Name = "Birth Date: ")]
        [DisplayFormat(NullDisplayText = "")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Country: ")]
        public string Country { get; set; }

        [Display(Name = "Team: ")]
        public Guid TeamId { get; set; }
        public IEnumerable<AdminBasicInfoViewModel> Teams { get; set; }
    }
}