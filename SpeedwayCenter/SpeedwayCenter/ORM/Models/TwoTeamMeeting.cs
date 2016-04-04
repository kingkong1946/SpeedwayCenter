using System.Collections.Generic;
using System.Linq;

namespace SpeedwayCenter.ORM.Models
{
    public class TwoTeamMeeting : Meeting
    {
        public int Round { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }

        public virtual ICollection<Rider> HomeTeamRiders { get; set; }
        public virtual ICollection<Rider> AwayTeamRiders { get; set; }

        public virtual Season Season { get; set; }

        public int HomeTeamPoints => HomeTeamRiders.Sum(rider => rider.GetTotalPointsFromMeeting(this));
        public int AwayTeamPoints => AwayTeamRiders.Sum(rider => rider.GetTotalPointsFromMeeting(this));

        public override string Name {
            get { return $"{HomeTeam.Name} - {AwayTeam.Name}"; }
            set { }
        }

        public string Score => $"{HomeTeamPoints} : {AwayTeamPoints}";
    }
}