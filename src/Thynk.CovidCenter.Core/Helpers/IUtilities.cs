using System;
using System.Threading.Tasks;
using Thynk.CovidCenter.Data.Models;

namespace Thynk.CovidCenter.Core.Helpers
{
    public interface IUtilities
    {
        Task<ApplicationUser> GetUser(Guid userId);
        Task<Location> GetLocation(Guid locationId);
    }
}
