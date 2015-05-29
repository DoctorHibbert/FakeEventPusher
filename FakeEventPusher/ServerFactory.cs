using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace FakeEventPusher
{
    internal static class ServerFactory
    {
        private const string CONNECTION_STRING = "Endpoint=sb://doctorhibbert-clinic-ns.servicebus.windows.net/;SharedAccessKeyName=Publisher;SharedAccessKey=Kg8xPbtxfTTU5ls1jrjX0ecgx2S37InQXhI9U6B9ADc=";

        public static IServer CreateServer()
        {
            var builder = new ServiceBusConnectionStringBuilder(CONNECTION_STRING)
            {
                TransportType = TransportType.Amqp
            };
            var client = EventHubClient.CreateFromConnectionString(builder.ToString(), "clinic");
            return new Server(client);
        }
    }
}
