using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Microsoft.AspNetCore.StaticFiles;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IPhysicalStorage _storage;

        public ContentController(IPhysicalStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("/games/{gameId}/images/{fileName}")]
        public async Task<IActionResult> GetImageAsync(int gameId, string fileName) 
        {
            var path = Path.Combine("games", gameId.ToString(), "images", fileName);
            var fullPath = await _storage.GetFilePathAsync(path);

            if (fullPath is null) 
            {
                return NotFound();
            }

            return PhysicalFile(fullPath);
        }

        [HttpGet("/temp/{fileName}")]
        public async Task<IActionResult> GetTempImageAsync(string fileName)
        {
            var path = Path.Combine("temp", fileName);
            var fullPath = await _storage.GetFilePathAsync(path);

            if (fullPath is null)
            {
                return NotFound();
            }

            return PhysicalFile(fullPath);
        }

        private IActionResult PhysicalFile(string path)
        {
            new FileExtensionContentTypeProvider().TryGetContentType(path, out var contentType);

            if (contentType is null) 
            {
                return BadRequest();
            }

            return PhysicalFile(path, contentType);
        }
    }
}
