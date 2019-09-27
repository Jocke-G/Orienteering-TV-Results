using OrienteeringTvResults.Model;
using OrienteeringTvResults.Model.Configuration;
using System;
using System.Net.Mqtt;
using System.Threading.Tasks;

namespace OrienteeringTvResults.Mqtt
{
    class MqttHandler
    {
        private static IMqttClient _client;
        private static MqttApplicationConfiguration _conf;

        public event EventHandler OnResendRequest;

        public async Task Initialize(MqttApplicationConfiguration configuration)
        {
            _conf = configuration;

            _client = await CreateClient();
            _client
                .MessageStream
                .Subscribe(msg =>
                {
                    OnMessageReceivedAsync(msg);
                });
            await ConnectAsync(_client, _conf);
            _client.Disconnected += OnDisconnectedAsync;

            MqttPublisher.Initialize(_client);
        }

        private void OnMessageReceivedAsync(MqttApplicationMessage msg)
        {
            switch(msg.Topic)
            {
                case "results/resend":
                    var e = new EventArgs();
                    EventHandler handler = OnResendRequest;
                    handler.Invoke(this, e);
                    break;
            }
        }

        private async Task<IMqttClient> CreateClient()
        {
            Logger.LogInfo($"Connecting to MQTT broker '{_conf.Host}'");

            var mqttConf = new MqttConfiguration();
            return await MqttClient.CreateAsync(_conf.Host, mqttConf);
        }

        private async Task ConnectAsync(IMqttClient client, MqttApplicationConfiguration conf)
        {
            await client.ConnectAsync(new MqttClientCredentials(conf.ClientId));
            await client.SubscribeAsync("results/resend", MqttQualityOfService.AtMostOnce);
        }

        private async void OnDisconnectedAsync(object sender, MqttEndpointDisconnected e)
        {
            Logger.LogError($"MQTT Disconnected. Reason: '{ e.Reason}', Message: '{e.Message}'");
            _client.Disconnected -= OnDisconnectedAsync;
            var delay = 5;
            while (!_client.IsConnected)
            {
                Logger.LogError("Trying to reconnect MQTT...");
                try
                {
                    await ConnectAsync(_client, _conf);
                }
                catch (Exception exception)
                {
                    Logger.LogError("Reconnect to MQTT failed", exception);
                    Logger.LogInfo($"Reconnecting to MQTT in {delay} seconds");
                }
                await Task.Delay(delay * 1000);
            }
            Logger.LogInfo("Successfully reconnected to MQTT");
            _client.Disconnected += OnDisconnectedAsync;
        }

        public async Task DisposeAsync()
        {
            await _client.DisconnectAsync();
            _client.Dispose();
        }
    }
}
