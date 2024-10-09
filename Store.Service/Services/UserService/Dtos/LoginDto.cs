using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.UserService.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
