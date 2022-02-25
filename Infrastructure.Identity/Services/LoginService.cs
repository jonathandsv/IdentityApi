using Infrastructure.Identity.Data.Requests;
using Infrastructure.Identity.Data.Response;
using Infrastructure.Identity.Extensions;
using Infrastructure.Identity.Interfaces.Services;
using Infrastructure.Identity.Models;
using Infrastructure.Shared.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Web;

namespace Infrastructure.Identity.Services
{
    public class LoginService : ILoginService
    {
        private SignInManager<ApplicationIdentityUser> _signInManager;
        private TokenService _tokenService;
        private AppSettings _appSettings;
        private IEmailService _emailService;

        public LoginService(SignInManager<ApplicationIdentityUser> signInManager, 
            TokenService tokenService, 
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public async Task<Response<LoginResponse>> UserLoginAsync(LoginRequest request)
        {
            
            var resultIdentity = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
            if (resultIdentity.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Email.ToUpper());

                var roles = await _signInManager.UserManager.GetRolesAsync(identityUser);
                var claims = await _signInManager.UserManager.GetClaimsAsync(identityUser);

                Token token = _tokenService
                    .CreateToken(identityUser, roles, claims);

                var response = new LoginResponse
                {
                    AccessToken = token.Value,
                    ExpiresIn = TimeSpan.FromHours(_appSettings.HoursToExpire).TotalSeconds,
                    UserToken = new UserTokenResponse
                    {
                        Id = identityUser.Id.ToString(),
                        Email = identityUser.Email,
                        UserName = $"{identityUser.FirstName} - {identityUser.LastName}",
                        Claims = claims.Select(c => new ClaimResponse { Type = c.Type, Value = c.Value })
                    }
                };

                return new Response<LoginResponse>(response, string.Empty);
            }
            return new Response<LoginResponse>(false, "Login falhou");
        }

        public async Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ApplicationIdentityUser identityUser = GetUserForEmail(request.Email);

            IdentityResult resultIdentity = await _signInManager
                .UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password);

            if (resultIdentity.Succeeded) 
                return new Response<string>(true, "Senha redefinida com sucesso!");

            return new Response<string>(false, "Houve um erro na operação");
        }

        public async Task<Response<string>> RequestResetPasswordAsync(RequestResetPasswordRequest request)
        {
            ApplicationIdentityUser userIdentity = GetUserForEmail(request.Email);

            if (userIdentity != null)
            {
                string codeReset = await _signInManager
                    .UserManager
                    .GeneratePasswordResetTokenAsync(userIdentity);

                var encodedCode = HttpUtility.UrlEncode(codeReset);

                await _emailService.SendEmailResetPasswordAsync(new[] { userIdentity.Email }, "Link de recuperacao de senha", userIdentity.Email, encodedCode);

                return new Response<string>(true, codeReset);
            }
            return new Response<string>(false, "Falha ao solicitar redefinição");
        }

        private ApplicationIdentityUser GetUserForEmail(string email)
        {
            return _signInManager
                .UserManager
                .Users
                .FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
