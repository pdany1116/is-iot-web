namespace IsIoTWeb.Settings
{
    public interface IMqttSettings
    {
        string BrokerHost { get; set; }
        int BrokerPort { get; set; }
        string User { get; set; }
        string Password { get; set; }
        bool WithCredentials { get; set; }
    }
}
