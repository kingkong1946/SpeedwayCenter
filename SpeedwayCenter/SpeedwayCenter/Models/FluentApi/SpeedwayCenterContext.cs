using System.Data.Entity;

namespace SpeedwayCenter.Models.FluentApi
{
    public class SpeedwayCenterContext : DbContext
    {
        public SpeedwayCenterContext() : base("name=SpeedwayCenterContext")
        {
        }

        public DbSet<Rider> Riders { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMeeting> TeamMeetings { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Temporarily
            modelBuilder.Ignore<Entity_Framework.Rider>();
            modelBuilder.Ignore<Entity_Framework.Team>();
            modelBuilder.Ignore<Entity_Framework.Meeting>();
            modelBuilder.Ignore<Entity_Framework.Score>();
            modelBuilder.Ignore<Entity_Framework.Heat>();

            modelBuilder.Entity<Rider>()
                .ToTable("Riders");
            modelBuilder.Entity<Rider>()
                .Ignore(e => e.ShortBirthDate);

            modelBuilder.Entity<Team>()
                .ToTable("Teams");
            modelBuilder.Entity<Team>()
                .Ignore(e => e.FullName);
            modelBuilder.Entity<Team>()
                .HasMany(e => e.Seasons)
                .WithMany(e => e.Teams);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}