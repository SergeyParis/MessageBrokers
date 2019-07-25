using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    public class ConfigFactory
    {
        public ChannelConfig GetChannelConfig() => new ChannelConfig
        {
            AutoAck = false,
            PrefetchSize = 0,
            PrefetchCount = 1
        };

        public ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount) => new ChannelConfig
        {
            AutoAck = autoAck,
            PrefetchCount = prefetchCount,
            PrefetchSize = 0
        };

        public ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount, int prefetchSize) =>
            new ChannelConfig
            {
                AutoAck = autoAck,
                PrefetchCount = prefetchCount,
                PrefetchSize = prefetchSize
            };

        public QueueConfig GetQueueConfig() => new QueueConfig
        {
            IsDurable = false
        };

        public QueueConfig GetQueueConfig(bool isDurable) => new QueueConfig
        {
            IsDurable = isDurable
        };
    }
}