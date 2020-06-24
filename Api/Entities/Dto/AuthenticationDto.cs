using System.ComponentModel.DataAnnotations;

namespace SAED.Api.Entities.Dto
{
    public class AuthenticationDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Remember { get; set; }
    }
}
