using System.Collections.Generic;
using Thynk.CovidCenter.Core.DTOs;

namespace Thynk.CovidCenter.Core.ResponseModel
{
    public class UserResponseModel : BaseResponse
    {
        public List<UserDTO> Data { get; set; }
    }
}
