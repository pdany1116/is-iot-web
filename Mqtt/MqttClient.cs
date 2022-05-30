using IsIoTWeb.Settings;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsIoTWeb.Mqtt
{
    public class MqttClient : IMqttClient
    {
        private readonly IMqttSettings _mqttSettings;
        private readonly MQTTnet.Client.IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;
        private string _lastPayload;

        public MqttClient(IMqttSettings mqttSettings)
        {
            _mqttSettings = mqttSettings;
            _mqttClient = new MqttFactory().CreateMqttClient();
            _mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(_mqttSettings.BrokerHost, _mqttSettings.BrokerPort)
                .WithCredentials(_mqttSettings.User, _mqttSettings.Password)
                .Build();
        }

        public async Task Connect()
        {
            if (!_mqttClient.IsConnected)
            {
                await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
            }
        }

        public async Task Disconnect()
        {
            if (_mqttClient.IsConnected)
            {
                await _mqttClient.DisconnectAsync();
            }
        }

        public async Task Publish(string topic, string payload)
        {
            if (!_mqttClient.IsConnected)
            {
                return;
            }

            var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithExactlyOnceQoS()
            .Build();

            await _mqttClient.PublishAsync(message, CancellationToken.None);
        }

        public async Task Subscribe(string topic)
        {
            if (!_mqttClient.IsConnected)
            {
                return;
            }

            await _mqttClient.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);
            _mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                try
                {
                    string topic = e.ApplicationMessage.Topic;

                    if (string.IsNullOrWhiteSpace(topic) == false)
                    {
                        _lastPayload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message, ex);
                }
            });
        }

        public string GetLastPayload()
        {
           Task.Run(() =>
            {
                while (_lastPayload == null)
                {
                    Task.Delay(25);
                }
            }).Wait(500);
            return _lastPayload;
        }
    }
}
