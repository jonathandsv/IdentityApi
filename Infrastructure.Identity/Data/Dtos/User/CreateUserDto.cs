using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity.Data.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string? RePassword { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public string? Img { get; set; }
        public string? UserName => Email;

    }
}
