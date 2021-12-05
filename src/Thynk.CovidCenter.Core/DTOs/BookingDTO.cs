using System;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.DTOs
{
    public class BookingDTO
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
        public Guid ID { get; set; }
    }
}
