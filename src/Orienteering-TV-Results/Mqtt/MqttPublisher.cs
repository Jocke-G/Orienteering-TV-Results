using System.Net.Mqtt;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.Mqtt
{
    public static class MqttPublisher
    {
        private static IMqttClient _client;

        public static void Initialize(IMqttClient client)
        {
            _client = client;
        }

        private static async Task PublishAsync(string topic, string payload)
        {
            var mqttMessage = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(payload));
            Logger.LogInfo($"Publishing MQTT message to topic: '{topic}'");
            await _client.PublishAsync(mqttMessage, MqttQualityOfService.AtMostOnce);
        }

        internal static async Task PublishAsync(CompetitionClass results)
        {
            var strMessages = JsonConvert.SerializeObject(results);

            await PublishAsync($"Results/Class/{results.ShortName}", strMessages);
        }
    }
}
