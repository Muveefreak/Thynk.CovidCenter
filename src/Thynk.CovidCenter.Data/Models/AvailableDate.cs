using System;
using System.Collections.Generic;

namespace Thynk.CovidCenter.Data.Models
{
    public class AvailableDate : BaseEntity
    {
        public Guid LocationId { get; set; }
        public DateTime DateAvailable { get; set; }
        public long AvailableSlots { get; set; }
        public bool Available{ get; set; }
        public virtual Location Location { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}
