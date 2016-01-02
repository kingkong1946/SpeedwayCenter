using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedwayCenter.Models.Entity_Framework
{
    public partial class Rider
    {
        private Image _photo;

        public Image Photo
        {
            get
            {
                Image tmp;
                if (_photo != null) tmp = _photo;
                using (var ms = new MemoryStream(Image))
                {
                    tmp = System.Drawing.Image.FromStream(ms);
                }
                return tmp;
            }
            set
            {
                _photo = value;
                using (var ms = new MemoryStream())
                {
                    _photo.Save(ms, ImageFormat.Png);
                    Image = ms.ToArray();
                }
            }
        }
    }
}
