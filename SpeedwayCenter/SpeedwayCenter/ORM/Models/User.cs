using System;
using System.Text;

namespace SpeedwayCenter.ORM.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}