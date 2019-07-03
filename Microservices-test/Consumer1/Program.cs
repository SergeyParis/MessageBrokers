using System;
using Infrastructure;
using Infrastructure.Brokers;

namespace Consumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbit = new RabbitMqClient(); 
//            rabbit.SubscribeOnQueue((model, arg) =>
//            {
//                var body = arg.Body;
//                Console.WriteLine(body.TransformToString());
//            });

            var result = rabbit.WaitMessageFromQueue();
            rabbit.Dispose();
            
            Console.WriteLine(result.TransformToString());
        }
    }
}