using BCPT.ABSTACTION;
using BCPT.BAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCPT.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpGet]
        [Route("clients")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<ICollection<Client>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clientResponse = await _clientService.GetClients();
                return StatusCode((int)clientResponse.Code, clientResponse);
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

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<ICollection<Client>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> FilterClients(string filterValue, string? filterBy = null, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var clientfilterResponse = await _clientService.FilterClients(new FilterRequest()
                {
                    FilterValue = filterValue,
                    FilterBy = filterBy,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
                return StatusCode((int)clientfilterResponse.Code, clientfilterResponse);
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

        [HttpGet]
        [Route("sort")]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response<ICollection<Client>>))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> SortClients(string sortBy, bool isAsc = true)
        {
            try
            {
                var clientfilterResponse = await _clientService.SortClients(sortBy, isAsc);
                return StatusCode((int)clientfilterResponse.Code, clientfilterResponse);
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> AddClient([FromBody] InsertClientRequest client)
        {
            try
            {
                var addClientResponse = await _clientService.AddClient(client);
                return StatusCode((int)addClientResponse.Code, addClientResponse);
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

        [HttpPatch]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] UpdateClientRequest updateClient)
        {
            try
            {
                var clientResponse = await _clientService.UpdateClient(id, updateClient);
                return StatusCode((int)clientResponse.Code, clientResponse);
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

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            try
            {
                var clientResponse = await _clientService.DeleteClient(id);
                return StatusCode((int)clientResponse.Code, clientResponse);
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
