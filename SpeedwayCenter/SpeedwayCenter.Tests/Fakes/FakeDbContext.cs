using System.Data.Entity;

namespace SpeedwayCenter.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        public virtual DbSet<FakeModel> Models { get; set; }
    }
}
