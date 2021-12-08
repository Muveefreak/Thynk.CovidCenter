using System;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.DTOs;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Core.ResponseModel;

namespace Thynk.CovidCenter.Core.Interface
{
    public interface IUserService
    {
        Task<GenericResponse<UserDTO>> CreateUser(CreateUserRequest request);
        Task<UserResponseModel> GetUsers();
        Task<GenericResponse<UserDTO>> GetUser(Guid userId);
    }
}
