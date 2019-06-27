using System;
using System.Text;
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
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            });

            Console.ReadKey();
        }
    }
}