using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.Models.Models;

namespace SpeedwayCenter.Models.Mapping
{
    public class HeatMap : EntityTypeConfiguration<Heat>
    {
        public HeatMap()
        {
            //Table
            ToTable("Heats");

            //Primary Key
            HasKey(e => e.Id);

            //Properties
            Property(e => e.GateAPoints)
                .IsOptional();

            Property(e => e.GateBPoints)
                .IsOptional();

            Property(e => e.GateCPoints)
                .IsOptional();

            Property(e => e.GateDPoints)
                .IsOptional();

            //Foreign Keys
            HasRequired(e => e.GateARider)
                .WithMany(r => r.Heats);

            HasRequired(e => e.GateBRider)
                .WithMany(r => r.Heats);

            HasRequired(e => e.GateCRider)
                .WithRequiredPrincipal();

            HasRequired(e => e.GateDRider)
                .WithRequiredPrincipal();

            
            HasOptional(e => e.GateASubstitution)
                .WithOptionalPrincipal();

            HasOptional(e => e.GateBSubstitution)
                .WithOptionalPrincipal();

            HasOptional(e => e.GateCSubstitution)
                .WithOptionalPrincipal();

            HasOptional(e => e.GateDSubstitution)
                .WithOptionalPrincipal();


            HasRequired(e => e.Meeting)
                .WithMany(e => e.Heats);
        }
    }
}