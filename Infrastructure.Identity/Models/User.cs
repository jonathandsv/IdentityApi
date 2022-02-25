namespace Infrastructure.Identity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Img { get; set; }
        public string UserName { get; set; }
    }
}
