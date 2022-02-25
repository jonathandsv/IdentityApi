using AutoMapper;
using Infrastructure.Identity.Data.Dtos.User;
using Infrastructure.Identity.Data.Requests;
using Infrastructure.Identity.Interfaces.Services;
using Infrastructure.Identity.Models;
using Infrastructure.Shared.Wrappers;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace Infrastructure.Identity.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IEmailService _emailService;

        public RegisterService(IMapper mapper, UserManager<ApplicationIdentityUser> userManager, 
            IEmailService emailService, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Response<string>> RegisterUserAsync(CreateUserDto createDto)
        {
            User user = _mapper.Map<User>(createDto);

            ApplicationIdentityUser userIdentity = _mapper.Map<ApplicationIdentityUser>(user);

            IdentityResult resultIdentity = await _userManager
                .CreateAsync(userIdentity, createDto.Password);

            await _userManager.AddToRoleAsync(userIdentity, "regular");

            if (resultIdentity.Succeeded)
            {
                string code = await SendActivationTokenAsync(userIdentity);

                return new Response<string>(true, code);
            }
            return new Response<string>(false, "Falha ao cadastrar usuário");
        }

        private async Task<string> SendActivationTokenAsync(ApplicationIdentityUser userIdentity)
        {
            var code = await _userManager
                                .GenerateEmailConfirmationTokenAsync(userIdentity);
            var encodedCode = HttpUtility.UrlEncode(code);

            await _emailService.SendConfirmationEmailAsync(new[] { userIdentity.Email }, "Link de Ativação", userIdentity.Email, encodedCode);
            return code;
        }

        public async Task<Response<string>> ActivateUserAccountAsync(ActiveAccountRequest request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email.ToUpper());

            var identityResult = await _userManager.ConfirmEmailAsync(identityUser, request.ActivationCode);

            if (identityResult.Succeeded)
            {
                return new Response<string>(true);
            }
            return new Response<string>(false, "Falha ao ativar conta de usuário");
        }

        public async Task<Response<string>> GenerateConfirmationTokenAsync(int userId)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == userId);
            if (identityUser != null)
            {
                string code = await SendActivationTokenAsync(identityUser);
                return new Response<string>(true, code);
            }
            return new Response<string>(false, "Usuário não encontrado");
        }
    }
}
