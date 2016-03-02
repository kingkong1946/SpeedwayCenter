namespace SpeedwayCenter.Models.Entity_Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SpeedwayContext : DbContext
    {
        public SpeedwayContext()
            : base("name=SpeedwayContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SpeedwayContext, SpeedwayCenter.Migrations.Configuration>("SpeedwayContext"));
        }

        public virtual DbSet<Rider> Riders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rider>()
                .Property(e => e.FirstName)
                .IsFixedLength();

            modelBuilder.Entity<Rider>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<Rider>()
                .Property(e => e.Country)
                .IsFixedLength();
        }
    }
}
