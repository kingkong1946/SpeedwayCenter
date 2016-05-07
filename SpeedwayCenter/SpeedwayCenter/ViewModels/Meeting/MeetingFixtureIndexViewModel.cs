using System;

namespace SpeedwayCenter.ViewModels.Meeting
{
    public class MeetingFixtureIndexViewModel
    {
        public Guid Id { get; }
        public string Date { get; }
        public string Title { get; }
        public string Score { get; }
        public int Round { get; }

        public MeetingFixtureIndexViewModel(Guid id, string date, string title, string score, int round)
        {
            Id = id;
            Date = date;
            Title = title;
            Score = score;
            Round = round;
        }
    }
}