using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels.Table
{
    public class TableIndexViewModel
    {
        public string Name { get; }

        [Display(Name = "M")]
        public int Matches { get; }

        [Display(Name = "W")]
        public int Wins { get; }

        [Display(Name = "D")]
        public int Draws { get; }

        [Display(Name = "L")]
        public int Loses { get; }

        [Display(Name = "P")]
        public int Points { get; }

        [Display(Name = "+/-")]
        public int PlusMinusPoints { get; }

        public TableIndexViewModel(string name, int matches, int wins, int draws, int loses, int points, int plusMinusPoints)
        {
            Name = name;
            Matches = matches;
            Wins = wins;
            Draws = draws;
            Loses = loses;
            Points = points;
            PlusMinusPoints = plusMinusPoints;
        }
    }
}