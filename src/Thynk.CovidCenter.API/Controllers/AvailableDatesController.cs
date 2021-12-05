using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Thynk.CovidCenter.API.Helpers;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using CoreBaseResponse = Thynk.CovidCenter.Core.ResponseModel.BaseResponse;

namespace Thynk.CovidCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailableDatesController : ControllerBase
    {
        private readonly IAvailableDatesService _availableDatesService;
        public AvailableDatesController(IAvailableDatesService availableDatesService)
        {
            _availableDatesService = availableDatesService;
        }

        [HttpPost]
        [Route("create-available-dates")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CreateAvailableDates(CreateAvailableDatesRequest request)
        {
            CoreBaseResponse response = await _availableDatesService.CreateDates(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("get-available-dates-by-location")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AvailableDatesResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetAvailableDatesByLocation(Guid locationId)
        {
            AvailableDatesResponseModel response = await _availableDatesService.GetDatesByLocation(locationId);
            return Ok(response);
        }

        [HttpPost]
        [Route("get-all-available-dates")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(AvailableDatesResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetAllAvailableDates()
        {
            AvailableDatesResponseModel response = await _availableDatesService.GetAllDates();
            return Ok(response);
        }
    }
}
