using System;
using System.Collections.Generic;
using SpeedwayCenter.Models.FluentApi;

namespace SpeedwayCenter.Models.Models
{
    public class Season
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public League League { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}