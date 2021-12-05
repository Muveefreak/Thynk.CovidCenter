using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface IAvailableDatesService
    {
        Task<BaseResponse> CreateDates(CreateAvailableDatesRequest request);
        Task<AvailableDatesResponseModel> GetDatesByLocation(Guid locationId);
        Task<AvailableDatesResponseModel> GetAllDates();
    }
}
