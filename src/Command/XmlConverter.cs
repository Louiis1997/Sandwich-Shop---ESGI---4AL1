using System.IO;
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
        T items = (T)_serializer.Deserialize(new XmlTextReader(fs));
        fs.Close();
        return items;

    }

    public string Serialize(T items)
    {
        StringWriter stringWriter = new StringWriter();
        _serializer.Serialize(stringWriter, items );
        return stringWriter.ToString();
    }
}