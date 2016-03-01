using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Services.Protocols;

namespace SpeedwayCenter.Models.Entity_Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Rider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "First Name")]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+ ", ErrorMessage = "Invalid characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+", ErrorMessage = "Invalid characters")]
        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        [RegularExpression("^[A-Za-z ]+", ErrorMessage = "Invalid characters")]
        public string Country { get; set; }
        
        [DisplayName("Photo")]
        public string Image { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";
    }
}
