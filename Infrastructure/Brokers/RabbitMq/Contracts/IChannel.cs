using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Contracts
{
    public interface IChannel
    {
        IModel MqChannel { get; }
        ChannelConfig Config { get; }
    }
}