using System;
using System.Collections.Generic;

namespace SpeedwayCenter.Models.FluentApi
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string StadiumName { get; set; }
        public int Capacity { get; set; }

        public ICollection<Season> Seasons { get; set; }
        public ICollection<Rider> Riders { get; set; }

        public string FullName => $"{Name} {City}";
    }
}