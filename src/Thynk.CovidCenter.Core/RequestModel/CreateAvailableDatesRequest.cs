using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateAvailableDatesRequest
    {
        [Required]
        public Guid ApplicationUserId { get; set; }
        public Guid LocationId { get; set; }
        public string DateAvailable { get; set; }
        public long AvailableSlots { get; set; }
    }
}
