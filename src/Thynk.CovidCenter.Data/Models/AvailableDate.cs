using System;

namespace Thynk.CovidCenter.Data.Models
{
    public class AvailableDate : BaseEntity
    {
        public Guid LocationId { get; set; }
        public Guid BookingId { get; set; }
        public DateTime DateAvailable { get; set; }
        public long AvailableSlots { get; set; }
        public bool Available{ get; set; }
        public virtual Location Locations { get; set; }
        public virtual Booking Bookings { get; set; }
    }
}
