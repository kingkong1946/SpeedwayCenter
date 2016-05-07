using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.Areas.Admin.ViewModels.Match
{
    public class AdminAddRidersMatchViewModel
    {
        [Display(Name = "1")]
        public Guid Rider1Id { get; set; }

        [Display(Name = "2")]
        public Guid Rider2Id { get; set; }

        [Display(Name = "3")]
        public Guid Rider3Id { get; set; }

        [Display(Name = "4")]
        public Guid Rider4Id { get; set; }

        [Display(Name = "5")]
        public Guid Rider5Id { get; set; }

        [Display(Name = "6")]
        public Guid Rider6Id { get; set; }

        [Display(Name = "7")]
        public Guid Rider7Id { get; set; }


        [Display(Name = "9")]
        public Guid Rider9Id { get; set; }

        [Display(Name = "10")]
        public Guid Rider10Id { get; set; }

        [Display(Name = "11")]
        public Guid Rider11Id { get; set; }

        [Display(Name = "12")]
        public Guid Rider12Id { get; set; }

        [Display(Name = "13")]
        public Guid Rider13Id { get; set; }

        [Display(Name = "14")]
        public Guid Rider14Id { get; set; }

        [Display(Name = "15")]
        public Guid Rider15Id { get; set; }

        public string Rider1 { get; set; }
        public string Rider2 { get; set; }
        public string Rider3 { get; set; }
        public string Rider4 { get; set; }
        public string Rider5 { get; set; }
        public string Rider6 { get; set; }
        public string Rider7 { get; set; }
               
        public string Rider9 { get; set; }
        public string Rider10 { get; set; }
        public string Rider11 { get; set; }
        public string Rider12 { get; set; }
        public string Rider13 { get; set; }
        public string Rider14 { get; set; }
        public string Rider15 { get; set; }

        public Guid HomeTeamId { get; set; }
        public string HomeTeam { get; set; }

        public Guid AwayTeamId { get; set; }
        public string AwayTeam { get; set; }

        public IEnumerable<AdminBasicInfoViewModel> HomeTeamRiders { get; set; }
        public IEnumerable<AdminBasicInfoViewModel> AwayTeamRiders { get; set; }
    }
}