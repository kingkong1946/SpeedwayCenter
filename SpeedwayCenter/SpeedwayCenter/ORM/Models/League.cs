using System;
using System.Collections.Generic;

namespace SpeedwayCenter.ORM.Models
{
    public class League
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
    }
}