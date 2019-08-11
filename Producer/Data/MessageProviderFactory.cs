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
                case ProviderType.Array: return new ArrayMessageProvider();
                case ProviderType.Numbers: return new NumbersMessageProvider();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    public enum ProviderType
    {
        Console,
        Hardcode,
        Array,
        Numbers
    }
}