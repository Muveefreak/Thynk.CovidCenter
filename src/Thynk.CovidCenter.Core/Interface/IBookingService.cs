using System.Threading.Tasks;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface IBookingService
    {
        Task<BookingResponseModel> GetBookings(BookingStatus status);
        Task<BaseResponse> CreateBooking(CreateBookingRequest request);
        Task<BaseResponse> CancelBooking(CreateBookingRequest request);
    }
}
