using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Thynk.CovidCenter.API.Helpers;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Data.Enums;
using CoreBaseResponse = Thynk.CovidCenter.Core.ResponseModel.BaseResponse;

namespace Thynk.CovidCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Route("get-booking")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CreateBooking(BookingStatus request)
        {
            CoreBaseResponse response = await _bookingService.GetBookings(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("create-booking")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest request)
        {
            CoreBaseResponse response = await _bookingService.CreateBooking(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("cancel-booking")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(CoreBaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationFailedResult))]
        public async Task<IActionResult> CancelBooking(CreateBookingRequest request)
        {
            CoreBaseResponse response = await _bookingService.CancelBooking(request);
            return Ok(response);
        }
    }
}
