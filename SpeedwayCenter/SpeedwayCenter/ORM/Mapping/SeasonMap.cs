using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class SeasonMap : EntityTypeConfiguration<Season>
    {
        public SeasonMap()
        {
            //Table
            ToTable("Seasons");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Name)
                .HasMaxLength(10)
                .IsRequired();

            //Foreign Key
            HasRequired(e => e.League)
                .WithMany(e => e.Seasons);

            HasMany(e => e.Teams)
                .WithMany(e => e.Seasons);

            HasMany(e => e.TwoTeamMeetings)
                .WithRequired(e => e.Season)
                .WillCascadeOnDelete(true);
        }
    }
}