using System.Text;
using Microsoft.ServiceBus.Messaging;

namespace FakeEventPusher
{
    internal class Server : IServer
    {
        private readonly EventHubClient _client;
        private bool _running;

        public Server(EventHubClient client)
        {
            _client = client;
        }

        public void Start()
        {
            _running = true;
            DoWork();
        }

        private void DoWork()
        {
            while (_running)
            {
                var json =
                    "{\"AccountId\":\"12312312-aee0-499d-a8f0-66e46e46a103\",\"Url\":\"http://someurl\",\"AttemptTime\":23040938,\"BlacklistCount\":67,\"StatusCode\":0,\"ConnectTime\":0,\"ResponseTime\":0,\"TimeOfAttempt\":\"2015-05-28T14:17:23.2640239Z\",\"RequestTime\":0,\"IsBlacklisted\":true}";
                var data = new EventData(Encoding.Unicode.GetBytes(json))
                {
                    PartitionKey = "fake-publisher"
                };

                data.Properties.Add("Type", "PushNotification");

                _client.Send(data);
            }
        }

        public void Stop()
        {
            _running = false;
        }
    }
}