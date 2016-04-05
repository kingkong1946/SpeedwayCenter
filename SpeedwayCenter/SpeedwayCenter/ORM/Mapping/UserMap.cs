using System.Data.Entity.ModelConfiguration;
using SpeedwayCenter.ORM.Models;

namespace SpeedwayCenter.ORM.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("Users");

            HasKey(m => m.Id);

            Property(m => m.UserName)
                .HasMaxLength(15)
                .IsRequired();

            Property(m => m.Password)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}