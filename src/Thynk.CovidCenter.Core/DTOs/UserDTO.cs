using System;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole UserRole { get; set; }
        public Guid ID { get; set; }
    }
}
