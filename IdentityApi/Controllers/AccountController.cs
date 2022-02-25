using Infrastructure.Identity.Data.Dtos.User;
using Infrastructure.Identity.Data.Requests;
using Infrastructure.Identity.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private ILoginService _loginService;
        private ILogoutService _logoutService;
        private IRegisterService _registerService;

        public AccountController(ILoginService loginService, ILogoutService logoutService, IRegisterService registerService)
        {
            _loginService = loginService;
            _logoutService = logoutService;
            _registerService = registerService;
        }


        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> LogaUsuario(LoginRequest request)
        {
            var resultado = await _loginService.UserLoginAsync(request);
            if (!resultado.Succeeded) return BadRequest(resultado);
            return Ok(resultado);
        }

        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestResetPassword(RequestResetPasswordRequest request)
        {
            var resultado = await _loginService.RequestResetPasswordAsync(request);
            if (!resultado.Succeeded) return BadRequest(resultado);
            return Ok(resultado);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var resultado = await _loginService.ResetPasswordAsync(request);
            if (!resultado.Succeeded) return BadRequest(resultado);
            return Ok(resultado);
        }

        #endregion

        #region Logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var resultado = await _logoutService.LogoutAsync();
            if (!resultado.Succeeded) return BadRequest(resultado);
            return Ok(resultado);
        }
        #endregion

        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto createDto)
        {
            var resultado = await _registerService.RegisterUserAsync(createDto);
            if (!resultado.Succeeded) return StatusCode(500);
            return Ok(resultado);
        }

        [HttpPost("active-account")]
        public async Task<IActionResult> ActivateUserAccount(ActiveAccountRequest request)
        {
            var resultado = await _registerService.ActivateUserAccountAsync(request);
            if (!resultado.Succeeded) return StatusCode(500);
            return Ok(resultado);
        }

        [HttpGet("resend-activation-code")]
        public async Task<IActionResult> ResendActionToken([FromQuery] SendConfirmationToken request)
        {
            var resultado = await _registerService.GenerateConfirmationTokenAsync(request.UserId);
            if (!resultado.Succeeded) return StatusCode(500);
            return Ok(resultado);
        }
        #endregion

    }
}
