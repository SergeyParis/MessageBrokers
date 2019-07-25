using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Interfaces
{
    public interface IChannel
    {
        IModel MqChannel { get; }
        ChannelConfig Config { get; }
    }
}