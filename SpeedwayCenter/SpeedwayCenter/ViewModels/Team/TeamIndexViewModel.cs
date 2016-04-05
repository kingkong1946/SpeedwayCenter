using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Team
{
    public class TeamIndexViewModel
    {
        [Display(Name = "Name")]
        public string FullName { get; }

        [Display(Name = "Stadium Name")]
        public string StadiumName { get; }

        public int Capacity { get; }

        public TeamIndexViewModel(string fullName, string stadiumName, int capacity)
        {
            FullName = fullName;
            StadiumName = stadiumName;
            Capacity = capacity;
        }
    }
}