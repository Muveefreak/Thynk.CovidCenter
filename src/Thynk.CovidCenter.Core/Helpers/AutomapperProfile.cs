using AutoMapper;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Data.Models;

namespace Thynk.CovidCenter.Core.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CreateUserRequest, ApplicationUser>();
        }
    }
}
