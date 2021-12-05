using System.Collections.Generic;
using Thynk.CovidCenter.Core.DTOs;

namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class ReportResponseModel : BaseResponse
    {
        public List<ReportDTO> Data { get; set; }
    }
}
