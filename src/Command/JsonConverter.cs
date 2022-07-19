using System.IO;
using System.Text.Json;

namespace sandwichshop.Command;

public class JsonConverter<T>
{
    public static string Serialize(T items)
    {
        return JsonSerializer.Serialize(items);
    }
    
    public static T Deserialize(string jsonPath)
    {
        using (StreamReader r = new StreamReader(jsonPath))
        {
            string json = r.ReadToEnd();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}