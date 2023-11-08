using System.ComponentModel.DataAnnotations;

namespace DienappApi.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
    }
}