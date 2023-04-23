using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Features.Identity
{
    public class RegisterUserRequestModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
