using System;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface IReportService
    {
        Task<BaseResponse> BookingResult(BookingResultRequest request);
        Task<ReportResponseModel> GetResults(GetResultsRequest request);
    }
}
