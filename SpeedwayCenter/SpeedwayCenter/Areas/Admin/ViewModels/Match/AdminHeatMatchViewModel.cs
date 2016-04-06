using System;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminHeatMatchViewModel
    {
        public int Number { get; set; }

        public string RiderGateA { get; set; }
        public Guid RiderIdGateA { get; set; }
        public int RiderScoreGateA { get; set; }

        public string RiderGateB { get; set; }
        public Guid RiderIdGateB { get; set; }
        public int RiderScoreGateB { get; set; }

        public string RiderGateC { get; set; }
        public Guid RiderIdGateC { get; set; }
        public int RiderScoreGateC { get; set; }

        public string RiderGateD { get; set; }
        public Guid RiderIdGateD { get; set; }
        public int RiderScoreGateD { get; set; }
    }
}