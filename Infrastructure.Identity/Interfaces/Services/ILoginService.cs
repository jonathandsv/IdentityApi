using Infrastructure.Identity.Data.Requests;
using Infrastructure.Identity.Data.Response;
using Infrastructure.Shared.Wrappers;

namespace Infrastructure.Identity.Interfaces.Services
{
    public interface ILoginService
    {
        Task<Response<LoginResponse>> UserLoginAsync(LoginRequest request);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordRequest request);
        Task<Response<string>> RequestResetPasswordAsync(RequestResetPasswordRequest request);
    }
}
