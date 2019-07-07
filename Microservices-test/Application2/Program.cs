using System;
using Infrastructure;
using Infrastructure.Brokers;
using Infrastructure.Brokers.RabbitMq;

namespace Application2
{
    class Program
    {
        private const string QueueName = "Microservices-test.Test";
        
        static void Main(string[] args)
        {
            var rabbit = new RabbitMqClient(); 
            rabbit.SubscribeOnQueue((model, arg) =>
            {
                var body = arg.Body;
                Console.WriteLine(body.TransformToString());
            }, QueueName);

            Console.WriteLine("=========");
            
            var result = rabbit.WaitMessageFromQueue(QueueName);
            Console.WriteLine(result.TransformToString());
            
            Console.ReadKey();
        }
    }
}