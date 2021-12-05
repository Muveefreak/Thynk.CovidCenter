using System;
using System.ComponentModel.DataAnnotations;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateBookingRequest
    {
        [Required]
        public Guid LocationID { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]
        public string IndividualName { get; set; }
        [Required]
        public DateTime AvailableDateSelected { get; set; }
    }
}
