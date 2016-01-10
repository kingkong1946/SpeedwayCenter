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
        [RegularExpression("^[A-Z][A-Za-z]+", ErrorMessage = "The first letter must be uppercase")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        [RegularExpression("^[A-Z][A-Za-z]+", ErrorMessage = "The first letter must be uppercase")]
        public string LastName { get; set; }

        [Column(TypeName = "datetime2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^[A-Z][A-Za-z]+", ErrorMessage = "The first letter must be uppercase")]
        public string Country { get; set; }

        //[RegularExpression("^\\w+\\.[A-Za-z]{3,4}")]
        [DisplayName("Photo")]
        public string Image { get; set; }

        [NotMapped]
        public string Name => $"{FirstName} {LastName}";

        //public byte[] Image { get; set; }
        //private Image _photo;
        //[NotMapped]
        //public Image Photo
        //{
        //    get
        //    {
        //        Image tmp;
        //        if (_photo != null) tmp = _photo;
        //        if (Image == null) return null;
        //        using (var ms = new MemoryStream(Image))
        //        {
        //            tmp = System.Drawing.Image.FromStream(ms);
        //        }
        //        return tmp;
        //    }
        //    set
        //    {
        //        _photo = value;
        //        using (var ms = new MemoryStream())
        //        {
        //            _photo.Save(ms, _photo.RawFormat);
        //            Image = ms.ToArray();
        //        }
        //    }
        //}
    }
}
