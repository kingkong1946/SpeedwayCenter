namespace SpeedwayCenter.Models.Entity_Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SpeedwayContext : DbContext
    {
        public SpeedwayContext()
            : base("name=Speedway")
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

            modelBuilder.Entity<Rider>()
                .HasOptional(e => e.Team)
                .WithMany(e => e.Riders);

            modelBuilder.Entity<Rider>()
                .HasMany(e => e.Scores)
                .WithOptional(e => e.Rider)
                .HasForeignKey(e => e.Rider);


            modelBuilder.Entity<Meeting>()
                .Property(e => e.City)
                .IsFixedLength();

            modelBuilder.Entity<Meeting>()
                .HasRequired(e => e.HomeTeam)
                .WithMany(e => e.HomeMeetings);

            modelBuilder.Entity<Meeting>()
                .HasRequired(e => e.AwayTeam)
                .WithMany(e => e.AwayMeetings);

            modelBuilder.Entity<Meeting>()
                .HasMany(e => e.Scores)
                .WithRequired(e => e.Meeting);


            modelBuilder.Entity<Scores>()
                .HasRequired(e => e.Rider)
                .WithMany(e => e.Scores);

            modelBuilder.Entity<Scores>()
                .HasRequired(e => e.Meeting)
                .WithMany(e => e.Scores);
        }
    }
}
