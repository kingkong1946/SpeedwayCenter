using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Rider
{
    public class RiderDetailsViewModel
    {
        public string Name { get; set; }
        public string Country { get; set; }

        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }

        public BasicInfoViewModel Team { get; set; }
        public IList<RiderMatchViewModel> Matches { get; set; }
    }
}