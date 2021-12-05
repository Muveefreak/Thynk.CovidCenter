using System;

namespace Thynk.CovidCenter.Repository.Exceptions
{
    public class CachingException : Exception
    {
        public CachingException() { }
        public CachingException(string message) : base(message) { }
    }
}
