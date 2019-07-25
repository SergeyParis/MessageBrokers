using Infrastructure.Brokers.RabbitMq.Models;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Contracts.Impl
{
    class Channel : IChannel
    {
        public IModel MqChannel { get; set; }
        public ChannelConfig Config { get; set; }
    }
}