using System;

namespace SpeedwayCenter.ViewModels.Rider
{
    public class RiderMatchViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Score { get; set; }
        public string Points { get; set; }
        public int Total { get; set; }
    }
}