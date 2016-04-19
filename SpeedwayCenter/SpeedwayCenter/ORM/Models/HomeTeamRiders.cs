using System;
using System.Linq;

namespace SpeedwayCenter.ORM.Models
{
    public class HomeTeamRiders
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public virtual TwoTeamMeeting Match { get; set; }
        public virtual Rider Rider { get; set; }

        public int GetTotalPointsFromMeeting(Meeting meeting)
        {
            return Rider.Results
                .Where(riderResult => riderResult.Meeting.Id == meeting.Id)
                .Sum(result => result.Points);
        }
    }
}