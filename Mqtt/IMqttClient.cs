using System.Threading.Tasks;

namespace IsIoTWeb.Mqtt
{
    public interface IMqttClient
    {
        Task Connect();

        Task Publish(string topic, string payload);

        Task Disconnect();
    }
}
