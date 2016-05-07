using System;

namespace SpeedwayCenter.ViewModels.Team
{
    public class TeamMatchViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Score { get; set; }
    }
}