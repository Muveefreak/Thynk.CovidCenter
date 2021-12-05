using System;
using System.Collections.Generic;

namespace Thynk.CovidCenter.Data.Models
{
    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Description { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<AvailableDate> AvailableDates { get; set; }
    }
}
