using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create-user")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GenericResponse<UserDTO>))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            GenericResponse<UserDTO> response = await _userService.CreateUser(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("get-users")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetUsers()
        {
            UserResponseModel response = await _userService.GetUsers();
            return Ok(response);
        }

        [HttpPost]
        [Route("get-user")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetUser([Required]Guid userId)
        {
            GenericResponse<Core.DTOs.UserDTO> response = await _userService.GetUser(userId);
            return Ok(response);
        }
    }
}
