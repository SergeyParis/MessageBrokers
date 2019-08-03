namespace Infrastructure.Brokers.RabbitMq.Contracts.Configs
{
    public struct MqConfig
    {
        public ChannelConfig ChannelConfig { get; set; }

        public QueueConfig QueueConfig { get; set; }
        
        public ExchangeConfig ExchangeConfig { get; set; }
    }
}