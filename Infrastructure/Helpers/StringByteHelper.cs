using System.Text;

namespace Infrastructure
{
    public static class StringByteHelper
    {
        public static byte[] TransformToByte(this string data) => Encoding.UTF8.GetBytes(data);
        
        public static string TransformToString(this byte[] data) => Encoding.UTF8.GetString(data);
    }
}