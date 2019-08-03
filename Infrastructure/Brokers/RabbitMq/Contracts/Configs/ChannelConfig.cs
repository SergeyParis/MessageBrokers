namespace Infrastructure.Brokers.RabbitMq.Contracts.Configs
{
    public struct ChannelConfig
    {
        /// <summary>
        /// Mark that ack will send directly after getting a message from the queue (without manual ack)
        /// </summary>
        public bool AutoAck { get; set; }
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