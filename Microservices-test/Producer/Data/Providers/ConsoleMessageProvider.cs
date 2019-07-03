using System;

namespace Producer.Data.Providers
{
    public class ConsoleMessageProvider : IMessageProvider
    {
        public string GetMessage(string[] args = null)
        {
            if (args == null)
                return "Hello world";
            
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}