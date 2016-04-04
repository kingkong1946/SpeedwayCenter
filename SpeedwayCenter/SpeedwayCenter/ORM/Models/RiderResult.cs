using System;
using SpeedwayCenter.Interface.Enums;

namespace SpeedwayCenter.ORM.Models
{
    public class RiderResult
    {
        public Guid Id { get; set; }
        public int Points { get; set; }
        public Gate Gate { get; set; }

        public virtual Rider Rider { get; set; }
        public virtual Heat Heat { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}