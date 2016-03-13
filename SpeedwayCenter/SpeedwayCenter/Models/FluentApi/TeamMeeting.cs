using System;

namespace SpeedwayCenter.Models.FluentApi
{
    public class TeamMeeting : Meeting
    {
        public int Round { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
    }
}