using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SpeedwayCenter.Models.Entity_Framework;

namespace SpeedwayCenter.Models.Repository
{
    public class RiderRepository<T> : Entity<Rider, T> where T : DbContext, new()
    {
        private Image _image;
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}