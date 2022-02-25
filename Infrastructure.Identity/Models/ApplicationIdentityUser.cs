using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class ApplicationIdentityUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Img { get; set; }
    }
}
