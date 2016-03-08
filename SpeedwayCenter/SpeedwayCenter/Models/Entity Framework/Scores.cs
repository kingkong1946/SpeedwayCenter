using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedwayCenter.Models.Entity_Framework
{
    [Table("Scores")]
    public class Scores
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [RegularExpression(@"(\d,)+(\d)")]
        public string Score { get; set; }
        
        [Required]
        [ForeignKey("RiderId")]
        public virtual Rider Rider { get; set; }

        [Required]
        [ForeignKey("MettingId")]
        public virtual Meeting Meeting { get; set; }
    }
}