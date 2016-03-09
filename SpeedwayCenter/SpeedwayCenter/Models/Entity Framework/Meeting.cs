using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;

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
    }
}