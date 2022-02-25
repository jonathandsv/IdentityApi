

using Infrastructure.Shared.Wrappers;

namespace Infrastructure.Identity.Interfaces.Services
{
    public interface ILogoutService
    {
        Task<Response<bool>> LogoutAsync();
    }
}
