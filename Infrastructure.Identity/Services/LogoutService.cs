using Infrastructure.Identity.Interfaces.Services;
using Infrastructure.Identity.Models;
using Infrastructure.Shared.Wrappers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    public class LogoutService : ILogoutService
    {
        private SignInManager<ApplicationIdentityUser> _signInManager;

        public LogoutService(SignInManager<ApplicationIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Response<bool>> LogoutAsync()
        {
            Task resultadoIdentity = _signInManager.SignOutAsync();
            if (resultadoIdentity.IsCompletedSuccessfully)
            {
                return new Response<bool>(true);
            }
            return new Response<bool>(false, "Logout falhou");
        }
    }
}
