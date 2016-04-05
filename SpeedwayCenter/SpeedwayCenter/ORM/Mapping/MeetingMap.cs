//using System.Data.Entity.ModelConfiguration;
//using SpeedwayCenter.ORM.Models;

//namespace SpeedwayCenter.ORM.Mapping
//{
//    public class MeetingMap : EntityTypeConfiguration<Meeting>
//    {
//        public MeetingMap()
//        {
//            //Table
//            ToTable("Meetings");

//            //Primary Key
//            HasKey(e => e.Id);

//            //Properties
//            Property(e => e.Date)
//                .IsRequired();

//            Property(e => e.Status)
//                .IsRequired();

//            //Foreign Keys
//            HasMany(e => e.Results)
//                .WithRequired(e => e.Meeting);
//        }
//    }
//}