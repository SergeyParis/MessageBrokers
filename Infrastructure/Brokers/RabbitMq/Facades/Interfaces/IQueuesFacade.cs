using System;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades.Interfaces
{
    public interface IQueuesFacade
    {
        void CreateQueue(string queueName);

        void ClearQueue(string queueName);

        void DeleteQueue(string queueName);

        void SubscribeOnQueue(Action<object, BasicDeliverEventArgs> handler, string queueName);
    }
}