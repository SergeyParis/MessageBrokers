namespace Producer.Data.Providers
{
    public class HardcodeMessageProvider : IMessageProvider
    {
        public string GetMessage() => "hello message";
    }
}