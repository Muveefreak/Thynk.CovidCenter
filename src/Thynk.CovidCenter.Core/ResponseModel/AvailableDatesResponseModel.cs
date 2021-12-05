using System.Collections.Generic;
using Thynk.CovidCenter.Core.DTOs;

namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class AvailableDatesResponseModel : BaseResponse
    {
        public List<AvailableDatesDTO> Data { get; set; }
    }
}
