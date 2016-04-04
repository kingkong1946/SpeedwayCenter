using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class TeamMap : EntityTypeConfiguration<Team>
    {
        public TeamMap()
        {
            //Table
            ToTable("Teams");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Name)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.City)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.StadiumName)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.Capacity)
                .IsRequired();

            //Foreign Keys
            HasMany(e => e.HomeMeetings)
                .WithRequired(e => e.HomeTeam)
                .WillCascadeOnDelete(false);

            HasMany(e => e.AwayMeetings)
                .WithRequired(e => e.AwayTeam)
                .WillCascadeOnDelete(false);

            HasMany(e => e.Riders)
                .WithMany(e => e.Teams)
                .Map(m => m.ToTable("TeamsRiders")
                    .MapLeftKey("TeamId")
                    .MapRightKey("RiderId"));
        }
    }
}