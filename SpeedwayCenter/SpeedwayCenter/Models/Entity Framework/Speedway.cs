namespace SpeedwayCenter.Models.Entity_Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Speedway : DbContext
    {
        public Speedway()
            : base("name=Speedway")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Speedway, SpeedwayCenter.Migrations.Configuration>("Speedway"));
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
