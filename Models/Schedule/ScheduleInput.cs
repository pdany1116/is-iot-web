using System.Collections.Generic;

namespace IsIoTWeb.Models.Schedule
{
    public class ScheduleInput
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public List<ScheduleEntry> Entries { get; set; }
        
    }
}
