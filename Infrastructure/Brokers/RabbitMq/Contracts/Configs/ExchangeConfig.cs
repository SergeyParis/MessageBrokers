namespace Infrastructure.Brokers.RabbitMq.Contracts.Configs
{
    public class ExchangeConfig
    {
        /// <summary>
        /// Resistant to Rabbit MQ Failures (Save messages to drive)
        /// </summary>
        public bool Durable { get; set; }

        public bool AutoDelete { get; set; }
    }
}