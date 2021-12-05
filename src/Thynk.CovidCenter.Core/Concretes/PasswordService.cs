using System.Text;
using Thynk.CovidCenter.Core.Interface;

namespace Thynk.CovidCenter.Core.Concretes
{
    public class PasswordService : IPasswordService
    {
        public bool PasswordCheck(string passwordClear, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordClear));
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
    }
}
