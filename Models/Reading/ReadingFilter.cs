namespace IsIoTWeb.Models
{
    public class ReadingFilter
    {
        public string? CollectorId { get; set; }
        public string? OneDate { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? PageSize { get; set; }
    }
}
