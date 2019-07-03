namespace Producer.Data.Providers
{
    public class ArrayMessageProvider : IMessageProvider
    {
        private string[] _data = null;
        private int _index = 0;
        private bool _dataSet = false;

        public string GetMessage(string[] args = null)
        {
            if (!_dataSet)
            {
                if (args == null)
                    _data = GetDefaultData();
                else
                    _data = args;
                _dataSet = true;
            }

            var result = _data[_index];
            IncrementIndex();

            return result;
        }

        private string[] GetDefaultData()
        {
            return new[]
            {
                "first",
                "second",
                "third"
            };
        }

        private void IncrementIndex()
        {
            _index++;
            _index = _index % _data.Length;
        }
    }
}