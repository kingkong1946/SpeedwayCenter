using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedwayCenter.Models.Entity_Framework
{
    public class Score
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@"([dutwDUTW\d],)*([dutwDUTW\d])")]
        public string Points { get; set; }

        public int RiderId { get; set; }
        public int MeetingId { get; set; }

        [Required]
        [ForeignKey("RiderId")]
        public virtual Rider Rider { get; set; }

        [Required]
        [ForeignKey("MeetingId")]
        public virtual Meeting Meeting { get; set; }
    }
}