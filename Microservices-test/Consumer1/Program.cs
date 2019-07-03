using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Brokers;
using Infrastructure.Brokers.RabbitMq;

namespace Consumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start c1");
            
            var rabbit = new RabbitMqClient();

            rabbit.SubscribeOnQueue((model, arg) =>
            {
                var message = arg.Body.TransformToString();
                
                int dots = message.Split('.').Length - 1;
                Thread.Sleep(dots * 1000);
                
                Console.WriteLine($" [{message}] Done");
            });

            // var result = rabbit.WaitMessageFromQueue();
            // Console.WriteLine(result.TransformToString());
            
            // rabbit.Dispose();
        }
    }
}