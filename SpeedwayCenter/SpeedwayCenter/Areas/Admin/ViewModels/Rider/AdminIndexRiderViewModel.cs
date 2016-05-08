using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Rider
{
    public class AdminIndexRiderViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime BirthDate { get; set; }
        
        public string Country { get; set; }
    }
}