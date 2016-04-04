using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class HeatMap : EntityTypeConfiguration<Heat>
    {
        public HeatMap()
        {
            //Table
            ToTable("Results");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.Number)
                .IsRequired();

            //Foreign Keys
            HasMany(e=>e.Gates)
                .WithRequired(e => e.Heat)
                .WillCascadeOnDelete(true);
            
            HasRequired(e => e.Meeting)
                .WithMany(e => e.Heats)
                .WillCascadeOnDelete(false);
        }
    }
}