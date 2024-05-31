
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.Controller.src
{
    [Route("api/v1/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _fileService;
        private readonly IConfiguration _configuration;

        public ImageController(IImageRepository fileService, IConfiguration configuration)
        {
            _fileService = fileService;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Upload([FromForm] ImageUploadModel form)
        {
            if (form == null || form.Image.Length == 0)
                return BadRequest("No image uploaded");

            var result = _fileService.SaveImage(form.Image);
            if (result.Item1 == 0)
                return BadRequest(result.Item2);
            var imageUrl = _configuration.GetSection("ImageBaseUrl").Value!;
            return Ok(new { url = imageUrl  + result.Item2 });
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            await _fileService.DeleteImage(fileName);
            return Ok(new { message = "Image deleted successfully" });
        }
    }
}