namespace Producer.Data
{
    public interface IMessageProvider
    {
        string GetMessage(string[] args = null);
    }
}