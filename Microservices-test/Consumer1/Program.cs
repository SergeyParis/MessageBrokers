using System;
using Infrastructure;
using Infrastructure.Brokers;
using Infrastructure.Brokers.RabbitMq;

namespace Consumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbit = new RabbitMqClient(); 
            
            rabbit.SubscribeOnQueue((model, arg) =>
            {
                var body = arg.Body;
                Console.WriteLine(body.TransformToString());
            });

//            var result = rabbit.WaitMessageFromQueue();
//            Console.WriteLine(result.TransformToString());
            
            rabbit.Dispose();
        }
    }
}