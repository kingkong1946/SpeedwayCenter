using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text.RegularExpressions;
using SpeedwayCenter.Helpers;

namespace SpeedwayCenter.Models.Entity_Framework
{
    public class Meeting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+")]
        public string City { get; set; }

        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        [Required]
        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }

        [Required]
        [ForeignKey("AwayTeamId")]
        public virtual Team AwayTeam { get; set; }

        public virtual ICollection<Score> Scores { get; set; }

        [NotMapped]
        public MeetingResult Result
        {
            get
            {
                int homePoints = 0;
                var homeTeamRiderIds = HomeTeam.Riders.Select(x => x.Id);
                foreach (var score in Scores)
                {
                    if (homeTeamRiderIds.Contains(score.RiderId))
                    {
                        var stringPoints = Regex.Replace(score.Points, @"\w", "0");
                        var singlePoints = stringPoints.Split(',');
                        var points = singlePoints.Select(int.Parse);
                    }
                }
            }
        }
    }
}