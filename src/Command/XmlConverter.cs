using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace sandwichshop.Command;

public class XmlConverter<T> where T : new()
{
    private readonly XmlSerializer _serializer;

    public XmlConverter()
    {
        _serializer = new XmlSerializer(typeof(T));
    }

    public T Deserialize(string xmlPath)
    {
        Stream fs = new FileStream(xmlPath, FileMode.Open);
        var items = (T)_serializer.Deserialize(new XmlTextReader(fs));
        fs.Close();
        return items;
    }

    public string Serialize(T items, string nameFile)
    {
        // Create folder if needed
        var path = nameFile;
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path) ??
                                      throw new InvalidOperationException("Path directory is null for path: " + path));

        var stringWriter = new StringWriter();
        _serializer.Serialize(stringWriter, items);
        File.WriteAllText(nameFile, stringWriter.ToString(), Encoding.UTF8);
        return stringWriter.ToString();
    }
}