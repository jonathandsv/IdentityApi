using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Data.Requests
{
    public class ActiveAccountRequest
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? ActivationCode { get; set; }
    }
}
