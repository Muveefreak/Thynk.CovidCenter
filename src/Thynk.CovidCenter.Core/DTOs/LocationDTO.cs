using System;

namespace Thynk.CovidCenter.Core.DTOs
{
    public class LocationDTO
    {
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Description { get; set; }
        public Guid ID { get; set; }
    }
}
