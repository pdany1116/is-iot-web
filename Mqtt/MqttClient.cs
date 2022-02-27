using IsIoTWeb.Settings;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading;
using System.Threading.Tasks;

namespace IsIoTWeb.Mqtt
{
    public class MqttClient : IMqttClient
    {
        private readonly IMqttSettings _mqttSettings;
        private readonly MQTTnet.Client.IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;

        public MqttClient(IMqttSettings mqttSettings)
        {
            _mqttSettings = mqttSettings;
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttSettings.BrokerHost, _mqttSettings.BrokerPort)
                //.WithCredentials(_mqttSettings.User, _mqttSettings.Password)
                .Build();
        }

        public async Task Connect()
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }

        public async Task Disconnect()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task Publish(string topic, string payload)
        {
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(payload)
                .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }
    }
}
