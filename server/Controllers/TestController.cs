using System.Security.Claims;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        IConfiguration _config;
        Cloudinary _cloudinary;
        public TestController(
            IConfiguration config,
            Cloudinary cloudinary
        )
        {
            _config = config;
            _cloudinary = cloudinary;
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

        [HttpGet("test-cloudinary")]
        public IActionResult TestCloudinary()
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"https://cloudinary-devs.github.io/cld-docs-assets/assets/images/cld-sample.jpg"),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return Ok(uploadResult);
        }
    }
}
