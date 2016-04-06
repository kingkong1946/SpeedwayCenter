using System;
using System.Collections.Generic;
using SpeedwayCenter.Interface.Enums;

namespace SpeedwayCenter.ORM.Models
{
    public abstract class Meeting
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Status Status { get; set; }
        public virtual string Name { get; set; }

        public virtual ICollection<Heat> Heats { get; set; }
        public virtual ICollection<RiderResult> RiderResults { get; set; }
        public virtual ICollection<Rider> Riders { get; set; }
    }
}