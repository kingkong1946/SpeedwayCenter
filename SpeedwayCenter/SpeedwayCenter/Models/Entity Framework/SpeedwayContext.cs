namespace SpeedwayCenter.Models.Entity_Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class SpeedwayContext : DbContext
    {
        public SpeedwayContext()
            : base("name=SpeedwayContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SpeedwayContext, SpeedwayCenter.Migrations.Configuration>("SpeedwayContext"));
        }

        public virtual DbSet<Rider> Riders { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Rider Entity
            modelBuilder.Entity<Rider>()
                .Property(e => e.FirstName)
                .IsFixedLength();

            modelBuilder.Entity<Rider>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<Rider>()
                .Property(e => e.Country)
                .IsFixedLength();

            //modelBuilder.Entity<Rider>()
            //    .HasOptional(e => e.Team)
            //    .WithMany(e => e.Riders);

            //modelBuilder.Entity<Rider>()
            //    .HasMany(e => e.Score)
            //    .WithOptional(e => e.Rider)
            //    .HasForeignKey(e => e.Rider);

            //// Meeting Entity
            //modelBuilder.Entity<Meeting>()
            //    .Property(e => e.City)
            //    .IsFixedLength();

            modelBuilder.Entity<Meeting>()
                .HasRequired(e => e.HomeTeam)
                .WithMany(e => e.HomeMeetings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Meeting>()
                .HasRequired(e => e.AwayTeam)
                .WithMany(e => e.AwayMeetings)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Meeting>()
            //    .HasMany(e => e.Score)
            //    .WithRequired(e => e.Meeting);

            //// Score Entity
            //modelBuilder.Entity<Score>()
            //    .HasRequired(e => e.Rider)
            //    .WithMany(e => e.Score);

            //modelBuilder.Entity<Score>()
            //    .HasRequired(e => e.Meeting)
            //    .WithMany(e => e.Score);

            //// Team Entity
            //modelBuilder.Entity<Team>()
            //    .HasMany(e => e.Riders)
            //    .WithOptional(e => e.Team);

            //modelBuilder.Entity<Team>()
            //    .HasMany(e => e.AwayMeetings)
            //    .WithRequired(e => e.AwayTeam);

            //modelBuilder.Entity<Team>()
            //    .HasMany(e => e.HomeMeetings)
            //    .WithRequired(e => e.HomeTeam);
        }
    }
}
