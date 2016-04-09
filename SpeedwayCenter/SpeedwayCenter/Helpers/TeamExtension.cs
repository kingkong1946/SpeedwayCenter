using System.Linq;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Helpers
{
    public static class TeamExtension
    {
        public static int GetPoints(this Team team)
        {
            int points = 0;

            var meetings = team.HomeMeetings.ToList();
            meetings.AddRange(team.AwayMeetings);

            //foreach (var meeting in meetings)
            //{
            //    var thisTeam = meeting.Scores
            //        .Where(x =>
            //        {
            //            var team.Riders.Select(x => x.Id)
            //        })
            //}

            return points;
        }
    }
}