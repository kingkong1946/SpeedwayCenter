using System;
using System.Collections.Generic;

namespace SpeedwayCenter.Models.FluentApi
{
    public class Rider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Forname { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}