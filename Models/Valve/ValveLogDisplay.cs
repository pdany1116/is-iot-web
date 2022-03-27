namespace IsIoTWeb.Models
{
    public class ValveLogDisplay
    {
        public int ValveId { get; set; }
        public double Timestamp { get; set; }
        public string Action { get; set; }
        public class UserInfo
        {
            public UserInfo(string id, string fullName)
            {
                Id = id;
                FullName = fullName;
            }
            public string Id { get; set; }
            public string FullName { get; set; }
        }
        public UserInfo User { get; set; }
    }
}
