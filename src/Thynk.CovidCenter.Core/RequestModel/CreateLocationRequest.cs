using System.ComponentModel.DataAnnotations;

namespace Thynk.CovidCenter.Core.RequestModel
{
    public class CreateLocationRequest
    {
        [Required]
        public string Name { get; set; }
        public bool Available { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        public string Description { get; set; }
    }
}
