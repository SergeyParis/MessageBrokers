using Infrastructure.Brokers.RabbitMq.Models;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    public static class ConfigFactory
    {
        public static ChannelConfig GetChannelConfig() => new ChannelConfig
        {
            AutoAck = false,
            PrefetchSize = 0,
            PrefetchCount = 1
        };

        public static ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount) => new ChannelConfig
        {
            AutoAck = autoAck,
            PrefetchCount = prefetchCount,
            PrefetchSize = 0
        };

        public static ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount, int prefetchSize) =>
            new ChannelConfig
            {
                AutoAck = autoAck,
                PrefetchCount = prefetchCount,
                PrefetchSize = prefetchSize
            };
    }
}