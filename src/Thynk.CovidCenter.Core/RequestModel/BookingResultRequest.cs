using System;
using System.ComponentModel.DataAnnotations;
using Thynk.CovidCenter.Data.Enums;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class BookingResultRequest
    {
        [Required]
        public Guid ID { get; set; }
        [EnumDataType(typeof(BookingResultType))]
        public BookingResultType BookingResult { get; set; }
    }
}
