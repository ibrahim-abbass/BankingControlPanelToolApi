using BCPT.ABSTACTION;
using BCPT.BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCPT.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            this._historyService = historyService;
        }

        [HttpGet]
        [Route("suggestions")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<List<string>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> GetSuggestions(int top = 3, string? propertyName = null)
        {
            try
            {
                var suggestions = await _historyService.GetTopNHistory(top, propertyName);

                return StatusCode((int)suggestions.Code, suggestions);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new Response
                {
                    Code = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                    Status = Status.Error
                });

            }
        }
    }
}