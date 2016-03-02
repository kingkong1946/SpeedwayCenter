using System.Data.Entity;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Tests.Fakes
{
    public class FakeDbContext : DbContext
    {
        public virtual DbSet<FakeModel> Models { get; set; }
    }
}
