namespace IsIoTWeb.Models
{
    public class IrrigationLogsFilter
    {
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? PageSize { get; set; }
    }
}
