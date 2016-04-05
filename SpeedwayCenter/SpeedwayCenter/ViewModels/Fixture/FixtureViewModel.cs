using System.Collections.Generic;
using SpeedwayCenter.ViewModels.Meeting;

namespace SpeedwayCenter.ViewModels.Fixture
{
    public class FixtureViewModel
    {
        public int RoundsCount { get; }
        public IEnumerable<MeetingFixtureIndexViewModel> Meetings { get; }

        public FixtureViewModel(int roundsCount, IEnumerable<MeetingFixtureIndexViewModel> meetings)
        {
            RoundsCount = roundsCount;
            Meetings = meetings;
        }
    }
}