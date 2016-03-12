using System;
using System.Collections.Generic;

namespace SpeedwayCenter.Models.FluentApi
{
    public class League
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<Season> Seasons { get; set; }
    }
}