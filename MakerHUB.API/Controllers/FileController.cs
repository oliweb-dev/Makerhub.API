using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakerHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public FileController(IHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(IFormFile file)
        {
            string path = $"{_env.ContentRootPath}Images";

            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                using FileStream stream = new FileStream($"{path}/{file.FileName}", FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            string path = $"{_env.ContentRootPath}Images/";

            try
            {
                //WebUtility.UrlDecode()
                FileStream stream = new FileStream($"{path}/{fileName}", FileMode.Open, FileAccess.Read);
                return File(stream, "image/jpg", true);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
