using System;
using System.Collections.Generic;
using SpeedwayCenter.Models.FluentApi;

namespace SpeedwayCenter.Models.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string StadiumName { get; set; }
        public int Capacity { get; set; }

        public ICollection<Meeting> HomeMeetings { get; set; }
        public ICollection<Meeting> AwayMeetings { get; set; }

        public ICollection<Season> Seasons { get; set; }
        public virtual ICollection<Rider> Riders { get; set; }
    }
}