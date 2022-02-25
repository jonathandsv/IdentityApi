using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExemploAutenticacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/teste-regular")]
        public IActionResult Get()
        {
            Retorno retorno = new Retorno() { ValorRetorno = "servicos para usuario regular" };
            return Ok(retorno);
        }

        [HttpGet("/teste-admin")]
        [Authorize(Roles = "admin")]
        public IActionResult TesteAdmin()
        {
            Retorno retorno = new Retorno() { ValorRetorno = "servicos para admin" };
            return Ok(retorno);
        }
    }

    public class Retorno
    {
        public string ValorRetorno { get; set; }
    }
}
