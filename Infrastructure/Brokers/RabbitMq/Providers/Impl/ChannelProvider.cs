using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using Infrastructure.Brokers.RabbitMq.Factories;

namespace Infrastructure.Brokers.RabbitMq.Providers.Impl
{
    internal class ChannelProvider : IChannelProvider
    {
        private const string LocalHostName = "localhost";
        private string _hostName;

        public ChannelConfig ChannelConfig { get; set; }
        
        public string Host
        {
            set => _hostName = value ?? LocalHostName;
            get => _hostName;
        }

        public ChannelProvider(string host, ChannelConfig config)
        {
            Host = host;
            ChannelConfig = config;
        }

        public IChannel GetChannel() => ChannelFactory.GetChannel(Host, ChannelConfig);
    }
}