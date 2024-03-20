using ApplicationsAudit.Core.DTOs.Requests;
using ApplicationsAudit.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DomofonAudit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : Controller
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("MoveInDashboard")]
        public async Task<IActionResult> MoveInDashBoard([FromForm] ApplicationMove move)
        {
            await _applicationService.MoveApplication(move.Status,
                move.ApplicationId, move.Index);
            return Ok();
        }
    }
}
