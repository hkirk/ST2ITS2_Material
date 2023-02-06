using System.Text.Json;

namespace FilesEx;

public class Serializations
{
    public static string SerializeAsJSon(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}