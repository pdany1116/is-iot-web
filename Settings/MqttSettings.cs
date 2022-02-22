namespace IsIoTWeb.Settings
{
    public class MqttSettings : IMqttSettings
    {
        public string BrokerHost { get; set; }
        public int BrokerPort { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
