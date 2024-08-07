using BCPT.ABSTACTION;
using BCPT.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BCPT.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(RegisterResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden, Type = typeof(RegisterResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            try
            {
                var registerResponse = await _authenticationService.Register(registerRequest);

                return StatusCode((int)registerResponse.Code, registerResponse);
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
        [Route("login")]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created, Type = typeof(LoginResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized, Type = typeof(LoginResponse))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, Type = typeof(Response))]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                var loginResponse = await _authenticationService.Login(loginRequest);

                return StatusCode((int)loginResponse.Code, loginResponse);
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
