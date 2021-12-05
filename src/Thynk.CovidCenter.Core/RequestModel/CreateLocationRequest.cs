using System;
using System.ComponentModel.DataAnnotations;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateLocationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid ApplicationUserId { get; set; }
    }
}
