namespace Infrastructure.Brokers.RabbitMq.Contracts.Configs
{
    public class ExchangeConfig
    {
        public bool Durable { get; set; }

        public bool AutoDelete { get; set; }
    }
}