using System;
using Infrastructure;
using Infrastructure.Brokers;

namespace Application1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbit = new RabbitMqClient(); 
            rabbit.PublishMessage("hello app2".TransformToByte());
            
            Console.ReadKey();
        }
    }
}