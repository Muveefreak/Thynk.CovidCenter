using Microsoft.AspNetCore.Mvc;
using System;
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
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        [Route("booking-result")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> BookingResult(BookingResultRequest request)
        {
            CoreBaseResponse response = await _reportService.BookingResult(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("get-results")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ReportResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> GetResults(GetResultsRequest request)
        {
            ReportResponseModel response = await _reportService.GetResults(request);
            return Ok(response);
        }
    }
}
