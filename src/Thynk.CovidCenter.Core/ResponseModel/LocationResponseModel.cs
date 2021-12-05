using System.Collections.Generic;
using Thynk.CovidCenter.Core.DTOs;

namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class LocationResponseModel : BaseResponse
    {
        public List<LocationDTO> Data { get; set; }
    }
}
