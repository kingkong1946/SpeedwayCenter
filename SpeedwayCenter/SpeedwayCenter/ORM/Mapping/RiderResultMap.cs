using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class RiderResultMap : EntityTypeConfiguration<RiderResult>
    {
        public RiderResultMap()
        {
            //Table
            ToTable("RiderResults");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Points)
                .IsOptional();

            Property(e => e.Gate)
                .IsRequired();

            //Foreign Key 
            HasOptional(e => e.Rider)
                .WithMany(e => e.Results);

            HasRequired(e => e.Heat)
                .WithMany(e => e.Gates);

            
        }
    }
}