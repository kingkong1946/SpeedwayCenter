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
        
        public virtual ICollection<Meeting> HomeMeetings { get; set; }
        public virtual ICollection<Meeting> AwayMeetings { get; set; }

        public virtual ICollection<Rider> Riders { get; set; }
    }
}