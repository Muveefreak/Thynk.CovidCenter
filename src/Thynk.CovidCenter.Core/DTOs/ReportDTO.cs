namespace Thynk.CovidCenter.Core.DTOs
{
    public class ReportDTO
    {
        public long BookingCapacity { get; set; }
        public long Bookings { get; set; }
        public long Tests { get; set; }
        public long PositiveCases { get; set; }
        public long NegativeCases { get; set; }
    }
}
