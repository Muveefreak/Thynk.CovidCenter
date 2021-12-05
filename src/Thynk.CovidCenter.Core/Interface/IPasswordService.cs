namespace Thynk.CovidCenter.Core.Interface
{
    public interface IPasswordService
    {
        bool PasswordCheck(string passwordClear, byte[] passwordSalt, byte[] passwordHash);
    }
}
