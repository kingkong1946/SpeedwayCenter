using System.Collections.Generic;
using SpeedwayCenter.ViewModels.Meeting;

namespace SpeedwayCenter.ViewModels.Fixture
{
    public class FixtureViewModel
    {
        public IEnumerable<int> Rounds { get; }
        public IEnumerable<MeetingFixtureIndexViewModel> Meetings { get; }

        public FixtureViewModel(IEnumerable<int> rounds, IEnumerable<MeetingFixtureIndexViewModel> meetings)
        {
            Rounds = rounds;
            Meetings = meetings;
        }
    }
}