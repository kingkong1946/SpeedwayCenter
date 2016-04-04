using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Rider
{
    public class AdminEditRiderViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name: ")]
        public string Name { get; set; }

        [Display(Name = "Forname: ")]
        public string Forname { get; set; }

        [Display(Name = "Birth Date: ")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Country: ")]
        public string Country { get; set; }

        [Display(Name = "Team: ")]
        public Guid TeamId { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> Teams { get; set; }
    }
}