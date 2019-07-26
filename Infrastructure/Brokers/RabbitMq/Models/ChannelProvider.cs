using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Factories;

namespace Infrastructure.Brokers.RabbitMq.Models
{
    class ChannelProvider : IChannelProvider
    {
        private const string LocalHostName = "localhost";
        
        private string _hostName;
        private ChannelConfig _channelConfig;

        public string Host
        {
            set => _hostName = value ?? LocalHostName;
            get => _hostName;
        }
        public ChannelConfig? ChannelConfig
        {
            set => _channelConfig = value ?? ConfigFactory.GetChannelConfig();
            get => _channelConfig;
        }

        public ChannelProvider(string host, ChannelConfig? config)
        {
            Host = host;
            ChannelConfig = config;
        }

        public IChannel GetChannel() => ChannelFactory.GetChannel(Host, ChannelConfig.Value);
    }
}