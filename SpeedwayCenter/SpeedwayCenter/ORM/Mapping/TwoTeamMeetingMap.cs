using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class TwoTeamMeetingMap : EntityTypeConfiguration<TwoTeamMeeting>
    {
        public TwoTeamMeetingMap()
        {
            //Table
            ToTable("TwoTeamMeetings");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Round)
                .IsRequired();

            //Foreign Keys
            HasRequired(e => e.HomeTeam)
                .WithMany(e => e.HomeMeetings)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.AwayTeam)
                .WithMany(e => e.AwayMeetings)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.Season)
                .WithMany(e => e.TwoTeamMeetings);

            HasMany(e => e.HomeTeamRiders)
                .WithRequired(r => r.Match);

            HasMany(e => e.AwayTeamRiders)
                .WithRequired(r => r.Match);
        }
    }
}