using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
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
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

    public static string Serialize(T items, string nameFile)
    {
        // Create folder if needed
        var path = nameFile;
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path) ??
                                      throw new InvalidOperationException("Path directory is null for path: " + path));

        var jsonString = JsonSerializer.Serialize(items, _options);
        File.WriteAllText(nameFile, jsonString, Encoding.UTF8);
        return jsonString;
    }

    public static T Deserialize(string jsonPath)
    {
        using (var r = new StreamReader(jsonPath))
        {
            var json = r.ReadToEnd();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}