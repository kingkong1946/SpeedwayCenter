using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedwayCenter.ORM.Models
{
    public class Rider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Forname { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        
        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<RiderResult> Results { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }

        public virtual ICollection<HomeTeamRiders> HomeMeetings { get; set; }
        public virtual ICollection<AwayTeamRiders> AwayMeetings { get; set; }

        public int GetTotalPointsFromMeeting(Meeting meeting)
        {
            return Results
                .Where(riderResult => riderResult.Meeting.Id == meeting.Id)
                .Sum(result => result.Points);
        }

        public string GetPointsFromMeeting(Meeting meeting)
        {
            return string.Join(",", Results
                .Where(result => result.Meeting.Id == meeting.Id)
                .Select(result => result.Points));
        }

        public string FullName => $"{Name} {Forname}";
    }
}