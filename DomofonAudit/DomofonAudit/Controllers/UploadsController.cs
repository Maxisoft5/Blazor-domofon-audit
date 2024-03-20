using ApplicationsAudit.Core.DTOs;
using ApplicationsAudit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DomofonAudit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadsController : Controller
    {
        private readonly IContactInfoService _contactInfoService;

        public UploadsController(IContactInfoService contactInfoService)
        {
            _contactInfoService = contactInfoService;
        }

        [HttpPost("uploadProfile")]
        public async Task<IActionResult> UploadProfile(IFormFile file, [FromQuery] int concatInfoID, CancellationToken token)
        {
            if (file.Length <= 0)
            {
                return BadRequest();
            }
            ContactInfoDTO info = null;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream, token);
                token.ThrowIfCancellationRequested();
                var bytes = stream.ToArray();
                var result = await _contactInfoService.UploadImageToManager(concatInfoID, bytes, token);
                if (!result.Success)
                {
                    return BadRequest();
                }
                info = result.ContactInfo;
            }
            return Ok();
        }

    }
}
