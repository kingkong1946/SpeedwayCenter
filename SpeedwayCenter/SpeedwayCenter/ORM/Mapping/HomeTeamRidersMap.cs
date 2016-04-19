using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class HomeTeamRidersMap : EntityTypeConfiguration<HomeTeamRiders>
    {
        public HomeTeamRidersMap()
        {
            // Primary Key
            HasKey(e => e.Id);

            // Properties
            Property(e => e.Number)
                .IsRequired();

            // Foreign Keys
            HasRequired(e => e.Match)
                .WithMany(e => e.HomeTeamRiders);

            HasRequired(e => e.Rider)
                .WithMany(e => e.HomeMeetings);

        }
    }
}