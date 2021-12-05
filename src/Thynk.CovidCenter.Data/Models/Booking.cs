using System;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Data.Models
{
    public class Booking : BaseEntity
    {
        public Guid LocationID { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateCancelled { get; set; }
        public DateTime? AvailableDateSelected { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public BookingResultType BookingResult { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid AvailableDatesId { get; set; }
        public string IndividualName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Location Locations { get; set; }
        public virtual AvailableDate AvailableDates { get; set; }
    }

}
