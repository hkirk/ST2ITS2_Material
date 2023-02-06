using System.Text;

namespace FilesEx;

public class Encodings
{
    public static byte[] ConvertToUtf8(string text)
    {
        // var utf8Encoding = new UTF8Encoding();
        // utf8Encoding.GetBytes(text);
        return Encoding.UTF8.GetBytes(text);
    }

    public static string ConvertFromUnicode(byte[] bytes)
    {
        return Encoding.Unicode.GetString(bytes);
    }
}