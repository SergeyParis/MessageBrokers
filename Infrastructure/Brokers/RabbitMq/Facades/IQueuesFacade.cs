using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Brokers.RabbitMq.Facades
{
    public interface IQueuesFacade
    {
        void CreateQueue(string queueName);

        void ClearQueue(string queueName);

        void DeleteQueue(string queueName);

        void SubscribeOnQueue(Action<IModel, BasicDeliverEventArgs> handler, string queueName);
    }
}