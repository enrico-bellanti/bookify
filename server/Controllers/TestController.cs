using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IConfiguration _config;
        public TestController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("config")]
        public IActionResult GetConfig()
        {

            var settings = new Dictionary<string, string>();
            foreach (var kvp in _config.AsEnumerable())
            {
                settings[kvp.Key] = kvp.Value;
            }
            return Ok(settings);
        }
    }
}
