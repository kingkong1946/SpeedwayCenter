using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class RiderMap : EntityTypeConfiguration<Rider>
    {
        public RiderMap()
        {
            //Table
            ToTable("Riders");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Name)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.Forname)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.Country)
                .HasMaxLength(20)
                .IsRequired();

            Property(e => e.BirthDate)
                .IsRequired();

            //Foreign Keys
            HasMany(e => e.Teams)
                .WithMany(e => e.Riders);

            HasMany(e => e.Results)
                .WithOptional(e => e.Rider)
                .WillCascadeOnDelete(true);

            HasMany(e => e.HomeMeetings)
                .WithRequired(e => e.Rider);

            HasMany(e => e.AwayMeetings)
                .WithRequired(e => e.Rider);
        }
    }
}