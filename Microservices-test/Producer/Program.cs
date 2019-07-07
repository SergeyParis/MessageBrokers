using System;
using System.Collections.Generic;
using System.Threading;
using Infrastructure;
using Infrastructure.Brokers.RabbitMq;
using Producer.Data;

namespace Producer
{
    class Program
    {
        private const string QueueName = "Microservices-test.Test";

        static void Main(string[] args)
        {
            var messageProvider = MessageProviderFactory.GetProvider(ProviderType.Numbers);
            var rabbit = new RabbitMqClient();
            
            while (true)
            {
                var message = messageProvider.GetMessage();
                
                Console.WriteLine($"Publish: {message}");
                rabbit.PublishMessage(message.TransformToByte(), QueueName);
                
                Thread.Sleep(300);
            }
        }
    }
}