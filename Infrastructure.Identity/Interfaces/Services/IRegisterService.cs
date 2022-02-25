using Infrastructure.Identity.Data.Dtos.User;
using Infrastructure.Identity.Data.Requests;
using Infrastructure.Shared.Wrappers;

namespace Infrastructure.Identity.Interfaces.Services
{
    public interface IRegisterService
    {
        Task<Response<string>> RegisterUserAsync(CreateUserDto createDto);
        Task<Response<string>> ActivateUserAccountAsync(ActiveAccountRequest request);
        Task<Response<string>> GenerateConfirmationTokenAsync(int usuarioId);
    }
}
