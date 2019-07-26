using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq.Contracts
{
    public interface IChannelProvider
    {
        string Host { get; set; }
        ChannelConfig? ChannelConfig { get; set; }

        IChannel GetChannel();
    }
}