using Infrastructure.Brokers.RabbitMq.Contracts.Configs;

namespace Infrastructure.Brokers.RabbitMq.Factories
{
    public static class ConfigFactory
    {
        #region Channels

        public static ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount, int prefetchSize) =>
            new ChannelConfig
            {
                AutoAck = autoAck,
                PrefetchCount = prefetchCount,
                PrefetchSize = prefetchSize
            };

        public static ChannelConfig GetChannelConfig(bool autoAck, int prefetchCount) =>
            GetChannelConfig(autoAck, prefetchCount, 0);

        public static ChannelConfig GetChannelConfig() => GetChannelConfig(false, 1);

        #endregion

        #region Queues

        public static QueueConfig GetQueueConfig(bool isDurable) => new QueueConfig
        {
            Durable = isDurable
        };

        public static QueueConfig GetQueueConfig() => GetQueueConfig(false);

        #endregion

        #region Exchanges

        public static ExchangeConfig GetExchangeConfig(bool durable, bool autoDelete) => new ExchangeConfig
        {
            Durable = durable,
            AutoDelete = autoDelete
        };

        public static ExchangeConfig GetExchangeConfig() => GetExchangeConfig(false, false);

        #endregion
    }
}