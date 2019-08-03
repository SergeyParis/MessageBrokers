using Infrastructure.Brokers.RabbitMq.Contracts;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;

namespace Infrastructure.Brokers.RabbitMq.Providers
{
    internal interface IChannelProvider
    {
        string Host { get; set; }
        ChannelConfig ChannelConfig { get; set; }

        IChannel GetChannel();
    }
}