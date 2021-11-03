using System.ComponentModel.DataAnnotations;

namespace API.Auth.DTOs.Requests
{
    public class RegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}