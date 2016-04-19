using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class AwayTeamRidersMap : EntityTypeConfiguration<AwayTeamRiders>
    {
        public AwayTeamRidersMap()
        {
            // Primary Key
            HasKey(e => e.Id);

            // Keys
            HasRequired(e => e.Match)
                .WithMany(e => e.AwayTeamRiders);

            HasRequired(e => e.Rider)
                .WithMany(e => e.AwayMeetings);

            // Properties
            Property(e => e.Number)
                .IsRequired();
        }
    }
}