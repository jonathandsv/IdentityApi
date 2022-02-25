using Infrastructure.Identity.Extensions;
using Infrastructure.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity.Services
{
    public class TokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        #region Token Old
        //public Token CreateToken(CustomIdentityUser usuario, string role)
        //{
        //    Claim[] direitosUsuario = new Claim[]
        //    {
        //        new Claim("username", usuario.UserName),
        //        new Claim("id", usuario.Id.ToString()),
        //        new Claim(ClaimTypes.Role, role),
        //        new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
        //    };

        //    var chave = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));

        //    var credenciaris = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //        claims: direitosUsuario,
        //        signingCredentials: credenciaris,
        //        expires: DateTime.UtcNow.AddHours(1)
        //        );
        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        //    return new Token(tokenString);
        //}
        #endregion

        public Token CreateToken(ApplicationIdentityUser usuario, IList<string> roles, IList<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, usuario.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim("nbf", ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            
            foreach (var userRole in roles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn");
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new Token(encodedToken);
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
