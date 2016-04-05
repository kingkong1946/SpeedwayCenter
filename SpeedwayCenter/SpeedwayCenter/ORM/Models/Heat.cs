using System;
using System.Collections.Generic;

namespace SpeedwayCenter.ORM.Models
{
    public class Heat
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public virtual Meeting Meeting { get; set; }

        public virtual ICollection<RiderResult> Gates { get; set; }
    }
}