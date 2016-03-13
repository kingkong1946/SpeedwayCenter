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
            modelBuilder.Ignore<Entity_Framework.Rider>();
            modelBuilder.Ignore<Entity_Framework.Team>();
            modelBuilder.Ignore<Entity_Framework.Meeting>();
            modelBuilder.Ignore<Entity_Framework.Score>();
            modelBuilder.Ignore<Entity_Framework.Heat>();
            //modelBuilder.Entity<Rider>().ToTable("Riders");
            //modelBuilder.Entity<Team>().ToTable("Teams");
            base.OnModelCreating(modelBuilder);
        }
    }
}