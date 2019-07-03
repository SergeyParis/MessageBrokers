﻿using System;
using Infrastructure;
using Infrastructure.Brokers;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var rabbit = new RabbitMqClient(); 
                rabbit.PublishMessage("hello app2-1".TransformToByte());

                Console.ReadKey();
            }
        }
    }
}