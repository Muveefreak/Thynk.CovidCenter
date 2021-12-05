using System.Threading.Tasks;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;
using Thynk.CovidCenter.Data.Models;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface IAuthenticationService
    {
        Task<GenericResponse<UserDTO>> Authenticate(AuthenticationRequest user);
    }
}
