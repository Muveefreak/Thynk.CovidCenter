using System.ComponentModel.DataAnnotations;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [EnumDataType(typeof(UserRole))]
        public UserRole UserRole { get; set; }
    }
}
