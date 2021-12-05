using System;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface ILocationService
    {
        Task<BaseResponse> CreateLocation(CreateLocationRequest request);
        Task<LocationResponseModel> GetLocations();
        Task<GenericResponse<LocationDTO>> GetLocation(Guid locationId);
    }
}
