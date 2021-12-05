using System.Collections.Generic;
using Thynk.CovidCenter.Core.DTOs;

namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class BookingResponseModel : BaseResponse
    {
        public List<BookingDTO> Data { get; set; }
    }
}
