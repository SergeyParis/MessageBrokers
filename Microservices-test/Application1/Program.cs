using System;
using Infrastructure.Brokers;

namespace Application1
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbit = new RabbitMqClient(); 
            rabbit.PublishMessage("hello app2");
            
            Console.ReadKey();
        }
    }
}