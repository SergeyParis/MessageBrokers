using System;
using Producer.Data.Providers;

namespace Producer.Data
{
    public static class MessageProviderFactory
    {
        public static IMessageProvider GetProvider(ProviderType type)
        {
            switch (type)
            {
                case ProviderType.Hardcode: return new HardcodeMessageProvider();
                case ProviderType.Console: return new ConsoleMessageProvider();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum ProviderType
    {
        Console,
        Hardcode
    }
}