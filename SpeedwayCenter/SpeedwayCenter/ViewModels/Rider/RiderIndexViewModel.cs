using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Rider
{
    public class RiderIndexViewModel
    {
        public string Name { get; } 
        public string Forname { get; }

        [Display(Name = "Birth Date")]
        public string BirthDate { get; }

        public string Country { get; }

        public RiderIndexViewModel(string name, string forname, string birthDate, string country)
        {
            Name = name;
            Forname = forname;
            BirthDate = birthDate;
            Country = country;
        }
    }
}