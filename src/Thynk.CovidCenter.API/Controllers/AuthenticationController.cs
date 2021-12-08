using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Thynk.CovidCenter.API.Helpers;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using CoreBaseResponse = Thynk.CovidCenter.Core.ResponseModel.BaseResponse;

namespace Thynk.CovidCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequest request)
        {
            GenericResponse<UserDTO> response = await _authService.Authenticate(request);

            return Ok(response);
        }
    }
}
