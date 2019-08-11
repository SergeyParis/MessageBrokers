using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Brokers.RabbitMq;
using Infrastructure.Brokers.RabbitMq.Contracts.Configs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer1
{
    class Program
    {
        private const string QueueName = "Microservices-test.Test";
        
        static void Main(string[] args)
        {
            Console.WriteLine("start c1");

            var rabbit = new RabbitMqClient();
            rabbit.CreateQueue(QueueName);
            
            rabbit.SubscribeOnQueue((channel, arg) =>
            {
                var message = arg.Body.TransformToString();
                
                Thread.Sleep(2000);
                Console.WriteLine($" [{message}] Done");

                rabbit.Ack(arg, channel);
            }, QueueName);
        }
    }
}