using Infrastructure.Brokers.RabbitMq.Configs;

namespace Infrastructure.Brokers.RabbitMq.Contracts
{
    internal interface IChannelProvider
    {
        string Host { get; set; }
        ChannelConfig? ChannelConfig { get; set; }

        IChannel GetChannel();
    }
}