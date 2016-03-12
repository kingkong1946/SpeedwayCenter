using System;

namespace SpeedwayCenter.Models.Entity_Framework
{
    public class Heat
    {
        public Guid Id { get; set; }
        public Rider GateA { get; set; }
        public Rider GateB { get; set; }
        public Rider GateC { get; set; }
        public Rider GateD { get; set; }

        public int GateAPoints { get; set; }
        public int GateBPoints { get; set; }
        public int GateCPoints { get; set; }
        public int GateDPoints { get; set; }

        public Meeting Meeting { get; set; }
    }
}