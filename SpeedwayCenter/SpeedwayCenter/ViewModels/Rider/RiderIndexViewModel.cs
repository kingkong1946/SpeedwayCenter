using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Rider
{
    public class RiderIndexViewModel
    {
        public Guid Id { get; }
        public string Name { get; } 

        [Display(Name = "Birth Date")]
        public string BirthDate { get; }

        public string Country { get; }

        public RiderIndexViewModel(Guid id, string name, string birthDate, string country)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Country = country;
        }
    }
}