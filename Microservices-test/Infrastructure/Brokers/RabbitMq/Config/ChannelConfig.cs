namespace Infrastructure.Brokers.RabbitMq.Config
{
    public class ChannelConfig
    {
        /// <summary>
        /// Maximal size of one message on bytes
        /// (0 - with no restrictions)
        /// </summary>
        public int PrefetchSize { get; set; }
        
        /// <summary>
        /// Count of messages that one consumer can receive at one time
        /// (if 1 that if consumer not ack one message that it doesn't get new)
        /// </summary>
        public int PrefetchCount { get; set; }
    }
}