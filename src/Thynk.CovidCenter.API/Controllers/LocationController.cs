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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        [Route("create-location")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CreateLocation(CreateLocationRequest request)
        {
            CoreBaseResponse response = await _locationService.CreateLocation(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("get-locations")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LocationResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetLocations()
        {
            LocationResponseModel response = await _locationService.GetLocations();
            return Ok(response);
        }

        [HttpPost]
        [Route("get-location")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetLocation([Required] Guid locationId)
        {
            GenericResponse<LocationDTO> response = await _locationService.GetLocation(locationId);
            return Ok(response);
        }
    }
}
