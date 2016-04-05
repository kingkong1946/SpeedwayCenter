using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class LeagueMap : EntityTypeConfiguration<League>
    {
        public LeagueMap()
        {
            //Table
            ToTable("Leagues");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

            Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(20);

            //Foreign Key
            HasMany(e=>e.Seasons)
                .WithRequired(e => e.League)
                .WillCascadeOnDelete(true);
        }
    }
}