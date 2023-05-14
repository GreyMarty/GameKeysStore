using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
using Microsoft.AspNetCore.StaticFiles;
using Application.UseCases.Keys.PurchaseKey;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text;
using MediatR;

namespace UI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly IPhysicalStorage _storage;
        private readonly IMediator _mediator;

        public ContentController(IPhysicalStorage storage, IMediator mediator)
        {
            _storage = storage;
            _mediator = mediator;
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

        [HttpPost("/games/{gameId}/purchase/{platformId}")]
        public async Task<IActionResult> PurchaseAsyns(int gameId, int platformId)
        {
            var key = await _mediator.Send(new PurchaseKeyCommand(gameId, platformId));

            var stream = new MemoryStream();
            await stream.WriteAsync(Encoding.UTF8.GetBytes(key.KeyString));

            return File(stream, "text/plain", $"{key.GameName}_Key.txt");
        }
    }
}
