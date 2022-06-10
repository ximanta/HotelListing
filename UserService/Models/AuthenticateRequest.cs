using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
