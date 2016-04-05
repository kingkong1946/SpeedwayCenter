namespace SpeedwayCenter.ViewModels.Meeting
{
    public class MeetingFixtureIndexViewModel
    {
        public string Date { get; }
        public string Title { get; }
        public string Score { get; }

        public MeetingFixtureIndexViewModel(string date, string title, string score)
        {
            Date = date;
            Title = title;
            Score = score;
        }
    }
}