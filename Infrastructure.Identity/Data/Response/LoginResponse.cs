namespace Infrastructure.Identity.Data.Response
{
    public class UserTokenResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IEnumerable<ClaimResponse> Claims { get; set; }
        public string ImgPerfil { get; set; }
        public int ChangePassword { get; set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenResponse UserToken { get; set; }
    }

    public class ClaimResponse
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

}