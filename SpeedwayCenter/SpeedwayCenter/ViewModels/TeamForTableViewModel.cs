namespace SpeedwayCenter.ViewModels
{
    public class TeamForTableViewModel
    {
        public string Name { get; }
        public int Meetings { get; }
        public int Points { get; }
        public int MiniPoints { get; }

        public TeamForTableViewModel(string name, int meetings, int points, int miniPoints)
        {
            Name = name;
            Meetings = meetings;
            Points = points;
            MiniPoints = miniPoints;
        }
    }
}