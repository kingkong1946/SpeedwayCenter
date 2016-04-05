using System.ComponentModel.DataAnnotations;

namespace SpeedwayCenter.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "User")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}