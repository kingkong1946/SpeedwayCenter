using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SpeedwayCenter.Interface;

namespace SpeedwayCenter.ORM.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string StadiumName { get; set; }
        public int Capacity { get; set; }


        public virtual ICollection<TwoTeamMeeting> HomeMeetings { get; set; }
        public virtual ICollection<TwoTeamMeeting> AwayMeetings { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<Rider> Riders { get; set; }

        public string FullName => $"{Name} {City}";

        public ICollection<TwoTeamMeeting> AllMeetings
        {
            get
            {
                if (HomeMeetings == null)
                {
                    return AwayMeetings;
                }
                if (AwayMeetings == null)
                {
                    return HomeMeetings;
                }
                return HomeMeetings.Concat(AwayMeetings).ToList();
            }
        }

        public int GetMatchCountFromSeason(Season season) => AllMeetings.Count(meeting => meeting.Season.Id == season.Id);

        public int GetPlusMinusPointsFromSeason(Season season)
        {
            int result = 0;

            result += HomeMeetings.Where(meeting => meeting.Season.Id == season.Id).Aggregate(0, (total, meeting) =>
            {
                int thisTeam = meeting.HomeTeamPoints;
                int otherTeam = meeting.AwayTeamPoints;

                total += thisTeam;
                total -= otherTeam;

                return total;
            });

            result += AwayMeetings.Where(meeting => meeting.Season.Id == season.Id).Aggregate(0, (total, meeting) =>
            {
                int thisTeam = meeting.AwayTeamPoints;
                int otherTeam = meeting.HomeTeamPoints;

                total += thisTeam;
                total -= otherTeam;

                return total;
            });

            return result;
        }

        public int GetStatisticsFromSeason(Season season, Func<int, int> predicate)
        {
            int result = 0;

            result += HomeMeetings.Where(meeting => meeting.Season.Id == season.Id).Aggregate(0, (total, meeting) =>
            {
                int homeTeam = meeting.HomeTeamPoints;
                int awayTeam = meeting.AwayTeamPoints;

                var comareResult = homeTeam.CompareTo(awayTeam);
                total += predicate(comareResult);

                return total;
            });

            result += AwayMeetings.Where(meeting => meeting.Season.Id == season.Id).Aggregate(0, (total, meeting) =>
            {
                int thisTeam = meeting.AwayTeamPoints;
                int otherTeam = meeting.HomeTeamPoints;

                var comareResult = thisTeam.CompareTo(otherTeam);
                total += predicate(comareResult);

                return total;
            });

            return result;
        }
    }
}