using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Data.Requests
{
    public class RequestResetPasswordRequest
    {
        [Required]
        public string? Email { get; set; }
    }
}
