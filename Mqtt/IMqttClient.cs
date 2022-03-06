using System.Threading.Tasks;

namespace IsIoTWeb.Mqtt
{
    public interface IMqttClient
    {
        Task Connect();

        Task Publish(string topic, string payload);

        Task Subscribe(string topic);

        string GetLastPayload();

        Task Disconnect();
    }
}
