using Infrastructure.Brokers.RabbitMq.Contracts;
using RabbitMQ.Client;

namespace Infrastructure.Brokers.RabbitMq.Models
{
    class Channel : IChannel
    {
        public IModel MqChannel { get; set; }
        public ChannelConfig Config { get; set; }
    }
}