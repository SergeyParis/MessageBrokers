namespace Infrastructure.Brokers.RabbitMq.Contracts.Enums
{
    public enum ExchangeType
    {
        /// <summary>
        /// Message Routing Key Used.
        /// The key name must exactly match the queue binding key.
        /// </summary>
        Direct,
        
        /// <summary>
        /// Use templates on message routing.
        /// </summary>
        Topic,
        
        /// <summary>
        /// Similar to topic.
        /// </summary>
        Headers,
        
        /// <summary>
        /// Publish message to all queues
        /// </summary>
        Fanout
    }
}