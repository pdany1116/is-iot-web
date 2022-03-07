namespace IsIoTWeb.Models
{
    public class ReadingFilter
    {
        public int? CollectorId { get; set; }
        public string? OneDate { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }
}
