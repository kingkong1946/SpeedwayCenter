using System;
using System.Collections.Generic;

namespace SpeedwayCenter.ORM.Models
{
    public class Season
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual League League { get; set; }
        public virtual ICollection<TwoTeamMeeting> TwoTeamMeetings { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}