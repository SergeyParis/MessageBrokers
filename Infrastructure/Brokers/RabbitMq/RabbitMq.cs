using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Factories;
using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq
{
    public static class RabbitMq
    {
        private const string LocalHostName = "localhost";

        public static IChannel GetOrCreateChannel(string host = LocalHostName, ChannelConfig? config = null)
        {
            if (config == null)
                config = ConfigFactory.GetChannelConfig();
            
            return ChannelFactory.GetOrCreateChannel(host, config.Value);
        }
    }
}