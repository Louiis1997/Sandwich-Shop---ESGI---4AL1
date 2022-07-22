using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sandwichshop.Command;

public class JsonConverter<T>
{
    private static readonly JsonSerializerOptions _options = 
        new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
        };
    public static string Serialize(T items, string nameFile)
    {
        string jsonString = JsonSerializer.Serialize(items, _options);
        File.WriteAllText(nameFile, jsonString);
        return jsonString;
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