using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Contracts
{
    internal interface IChannel
    {
        IModel MqChannel { get; }
        ChannelConfig Config { get; }
    }
}