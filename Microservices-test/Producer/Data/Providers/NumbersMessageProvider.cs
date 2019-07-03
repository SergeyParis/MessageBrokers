namespace Producer.Data.Providers
{
    public class NumbersMessageProvider : IMessageProvider
    {
        private int _value = 0;
        
        public string GetMessage(string[] args = null) => $"Message - {_value++}";
        
    }
}