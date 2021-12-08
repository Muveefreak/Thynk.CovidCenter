using System;
using System.ComponentModel.DataAnnotations;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CancelBookingRequest
    {
        [Required]
        public Guid LocationID { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
        [Required]
        public Guid AvailableDateId { get; set; }
        //[Required]
        //public DateTime AvailableDateSelected { get; set; }
        //[EnumDataType(typeof(TestType))]
        //public TestType TestType { get; set; }
    }
}
