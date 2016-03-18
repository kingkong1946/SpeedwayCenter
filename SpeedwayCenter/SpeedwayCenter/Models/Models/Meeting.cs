using System;
using System.Collections.Generic;
using SpeedwayCenter.Interface;

namespace SpeedwayCenter.Models.Models
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Round { get; set; }
        public Status Status { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public ICollection<Heat> Heats { get; set; }
    }
}