using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Data.Models
{
    public class ApplicationUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public UserRole UserRole { get; set; }
    }

}
