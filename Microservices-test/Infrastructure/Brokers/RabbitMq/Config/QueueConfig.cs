namespace Infrastructure.Brokers.RabbitMq.Config
{
    public class QueueConfig
    {
        /// <summary>
        /// Resistant to Rabbit MQ Failures (Save messages to drive)
        /// </summary>
        public bool IsDurable { get; set; }
    }
}