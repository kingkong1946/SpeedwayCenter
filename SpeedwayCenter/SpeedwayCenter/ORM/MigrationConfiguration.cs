using System.Data.Entity.Migrations;

namespace SpeedwayCenter.ORM
{
    internal sealed class MigrationConfiguration : DbMigrationsConfiguration<SpeedwayCenterContext>
    {
        public MigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}