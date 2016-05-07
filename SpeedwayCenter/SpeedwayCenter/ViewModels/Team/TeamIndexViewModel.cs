using System;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Team
{
    public class TeamIndexViewModel
    {
        public Guid Id { get; }
        [Display(Name = "Name")]
        public string FullName { get; }

        [Display(Name = "Stadium Name")]
        public string StadiumName { get; }

        public int Capacity { get; }

        public TeamIndexViewModel(Guid id, string fullName, string stadiumName, int capacity)
        {
            Id = id;
            FullName = fullName;
            StadiumName = stadiumName;
            Capacity = capacity;
        }
    }
}