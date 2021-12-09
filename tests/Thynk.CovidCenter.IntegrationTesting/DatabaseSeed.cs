using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thynk.CovidCenter.Core.RequestModel;
using Thynk.CovidCenter.Data.Enums;
using Thynk.CovidCenter.Data.Models;
using Thynk.CovidCenter.Repository;

namespace Thynk.CovidCenter.IntegrationTesting
{
    public static class DatabaseSeed
    {
        public static void SeedData(CovidCenterDbContext _appContext)
        {
            var userRequest = new List<ApplicationUser>
            {
                GenerateUserRequest("AdminUser", "admin@test.com", UserRole.Administrator),
                GenerateUserRequest("LabAdminUser", "labadmin@test.com", UserRole.LabAdministrator),
                GenerateUserRequest("Individual", "individual@test.com", UserRole.Individual)
            };

            _appContext.AddRange(userRequest);

            var locations = new List<Location>
            {
                GenerateLocationRequest("Location1", "Location1Description"),
                GenerateLocationRequest("Location2", "Location2Description"),
                GenerateLocationRequest("Location3", "Location3Description")
            };

            _appContext.AddRange(locations);
            _appContext.SaveChanges();

            var locationsEntity = _appContext.Set<Location>().ToList();

            var availableDates = new List<AvailableDate>
            {
                GenerateAvailableDateRequest(locationsEntity[0].ID, DateTime.UtcNow.AddDays(3), 10, true),
                GenerateAvailableDateRequest(locationsEntity[1].ID, DateTime.UtcNow.AddDays(4), 0, false),
                GenerateAvailableDateRequest(locationsEntity[2].ID, DateTime.UtcNow.AddDays(5), 5, true)
            };

            _appContext.AddRange(availableDates);
            _appContext.SaveChanges();

            var applicationUsersEntity = _appContext.Set<ApplicationUser>().ToList();
            var availableDatesEntity = _appContext.Set<AvailableDate>().ToList();

            var bookings = new List<Booking>
            {
                GenerateBookingRequest(locationsEntity[0].ID, availableDatesEntity[0].DateAvailable.Date, TestType.PCR, applicationUsersEntity[2].ID,availableDatesEntity[0].ID),
                GenerateBookingRequest(locationsEntity[1].ID, availableDatesEntity[1].DateAvailable.Date, TestType.PCR, applicationUsersEntity[2].ID,availableDatesEntity[2].ID),
                GenerateBookingRequest(locationsEntity[2].ID, availableDatesEntity[2].DateAvailable.Date, TestType.PCR, applicationUsersEntity[2].ID,availableDatesEntity[2].ID)
            };

            _appContext.AddRange(bookings);

            _appContext.SaveChanges();

        }

        private static ApplicationUser GenerateUserRequest(string userName, string email, UserRole userRole)
        {
            return new ApplicationUser
            {
                UserName = userName,
                Email = email,
                UserRole = userRole
            };
        }

        private static Location GenerateLocationRequest(string name, string description)
        {
            return new Location
            {
                Name = name,
                Description = description
            };
        }

        private static AvailableDate GenerateAvailableDateRequest(Guid locationId, DateTime dateAvailable, long availableSlots, 
            bool available)
        {
            return new AvailableDate
            {
                LocationId = locationId,
                DateAvailable = dateAvailable,
                AvailableSlots= availableSlots,
                Available = available
            };
        }

        private static Booking GenerateBookingRequest(Guid locationId, DateTime dateAvailable,TestType testType, 
            Guid applicationUserId, Guid availableDateId)
        {
            return new Booking
            {
                LocationID = locationId,
                DateCreated = DateTime.Now.Date,
                AvailableDateSelected = dateAvailable,
                BookingStatus = BookingStatus.Pending,
                TestType = testType,
                BookingResult = BookingResultType.None,
                ApplicationUserId = applicationUserId,
                AvailableDateId = availableDateId
            };
        }
    }
}
