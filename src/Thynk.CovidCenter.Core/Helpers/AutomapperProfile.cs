using AutoMapper;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Data.Models;

namespace Thynk.CovidCenter.Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CreateUserRequest, UserDTO>();
            CreateMap<CreateUserRequest, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<CreateAvailableDatesRequest, AvailableDate>();
            CreateMap<AvailableDate, AvailableDatesDTO>();
            CreateMap<Booking, BookingDTO>();
            CreateMap<CreateLocationRequest, Location>();
            CreateMap<Location, LocationDTO>();
        }
    }
}
