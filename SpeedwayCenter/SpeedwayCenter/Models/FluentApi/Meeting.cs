using System;
using System.Collections.Generic;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.FluentApi
{
    public abstract class Meeting
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Heat> Heats { get; set; }
    }
}