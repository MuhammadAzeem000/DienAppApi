using System.ComponentModel.DataAnnotations;

namespace DienappApi.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }
    }
}