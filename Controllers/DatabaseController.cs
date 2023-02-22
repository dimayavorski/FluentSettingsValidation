using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FluentSettingsValidation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DatabaseController> _logger;
        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public DatabaseController(ILogger<DatabaseController> logger, IOptions<DatabaseSettings> _settings)
        {
            _logger = logger;
            _databaseSettings = _settings;
        }

        [HttpGet]
        public string Get()
        {
            return $"{_databaseSettings.Value}_{_databaseSettings.Value.RetryInterval}";
        }
    }
}