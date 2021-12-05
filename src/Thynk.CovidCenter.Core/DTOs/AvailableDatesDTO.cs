using System;

namespace Thynk.CovidCenter.Core.DTOs
{
    public class AvailableDatesDTO
    {
        public Guid LocationId { get; set; }
        public Guid BookingId { get; set; }
        public DateTime DateAvailable { get; set; }
        public long AvailableSlots { get; set; }
        public bool Available { get; set; }
    }
}
