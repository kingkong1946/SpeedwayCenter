using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedwayCenter.Models.Entity_Framework
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+", ErrorMessage = "Invalid characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+", ErrorMessage = "Invalid characters")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Stadium Name")]
        public string StadiumName { get; set; }

        [Required]
        public int Capacity { get; set; }

        public virtual ICollection<Meeting> HomeMeetings { get; set; }
        public virtual ICollection<Meeting> AwayMeetings { get; set; }

        public virtual ICollection<Rider> Riders { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        public string FullName => $"{Name} {City}";
    }
}