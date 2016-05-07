using System.Collections.Generic;
using SpeedwayCenter.ViewModels.Rider;

namespace SpeedwayCenter.ViewModels.Meeting
{
    public class MatchHeatViewModel
    {
        public int Number;
        public HeatRiderViewModel GateA { get; set; }
        public HeatRiderViewModel GateB { get; set; }
        public HeatRiderViewModel GateC { get; set; }
        public HeatRiderViewModel GateD { get; set; }
    }
}