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
        public TestType TestType { get; set; }
        public BookingResultType BookingResult { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid AvailableDateId { get; set; }
        public string IndividualName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Location Location { get; set; }
        public virtual AvailableDate AvailableDate { get; set; }
    }

}
