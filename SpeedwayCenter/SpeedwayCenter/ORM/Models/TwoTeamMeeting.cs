using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SpeedwayCenter.ORM.Models
{
    public class TwoTeamMeeting : Meeting
    {
        public int Round { get; set; }

        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }

        public virtual ICollection<HomeTeamRiders> HomeTeamRiders { get; set; }
        public virtual ICollection<AwayTeamRiders> AwayTeamRiders { get; set; }

        public virtual Season Season { get; set; }

        public int HomeTeamPoints => HomeTeamRiders.Sum(rider => rider.GetTotalPointsFromMeeting(this));
        public int AwayTeamPoints => AwayTeamRiders.Sum(rider => rider.GetTotalPointsFromMeeting(this));

        public override string Name {
            get { return $"{HomeTeam?.FullName} - {AwayTeam?.FullName}"; }
            set { }
        }

        public string Score => $"{HomeTeamPoints}:{AwayTeamPoints}";

        //public override ICollection<Rider> Riders
        //{
        //    get
        //    {
        //        if (HomeTeam == null) return AwayTeamRiders;
        //        if (AwayTeam == null) return HomeTeamRiders.Select(r => r.Rider).ToList();
        //        return new Collection<Rider>(HomeTeamRiders
        //            .Select(r => r.Rider)
        //            .Concat(AwayTeamRiders)
        //            .ToList());
        //    }
        //    set { }
        //}
    }
}