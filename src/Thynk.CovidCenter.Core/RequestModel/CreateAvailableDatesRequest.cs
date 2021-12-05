using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateAvailableDatesRequest
    {
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public List<DateTime> DatesAvailable { get; set; }
    }
}
