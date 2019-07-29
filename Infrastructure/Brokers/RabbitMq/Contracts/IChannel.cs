using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Contracts
{
    internal interface IChannel
    {
        IModel MqChannel { get; }
        ChannelConfig Config { get; }
    }
}