using System;
using System.Collections.Generic;

namespace Thynk.CovidCenter.Data.Models
{
    public class AvailableDate : BaseEntity
    {
        public AvailableDate()
        {
            Bookings = new HashSet<Booking>();
        }
        public Guid LocationId { get; set; }
        public Guid BookingId { get; set; }
        public DateTime DateAvailable { get; set; }
        public long AvailableSlots { get; set; }
        public bool Available{ get; set; }
        public virtual Location Locations { get; set; }
        public ICollection<Booking> Bookings { get; private set; }
    }
}
