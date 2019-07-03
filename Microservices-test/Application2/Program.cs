using System;
using Infrastructure;
using Infrastructure.Brokers;

namespace Application2
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

            Console.WriteLine("ads");
            
//            var result = rabbit.WaitMessageFromQueue();
//            Console.WriteLine(result.TransformToString());
//            
//            Console.ReadKey();
        }
    }
}