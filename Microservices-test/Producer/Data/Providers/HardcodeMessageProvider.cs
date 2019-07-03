namespace Producer.Data.Providers
{
    public class HardcodeMessageProvider : IMessageProvider
    {
        public string GetMessage(string[] args = null) => "hello message";
    }
}