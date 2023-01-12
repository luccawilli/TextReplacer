using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace TextReplacer.Service {
  public class XmlSerializerHelper {

    //----------------------------------------------------------------------------------------------------
    /// <summary>Deserialize the given file.</summary>
    /// <returns>The deserialized content.</returns>
    public T DeserializeXMLFile<T>(String fileName) {
      T deserilizedObject;
      XmlSerializer ser = new XmlSerializer(typeof(T));
      using (var reader = Assembly.GetAssembly(GetType()).GetManifestResourceStream(GetType(), fileName)) {
        deserilizedObject = (T)ser.Deserialize(reader);
      }
      return deserilizedObject;
    }

    //----------------------------------------------------------------------------------------------------
    /// <summary>Serialize the given object to string..</summary>
    /// <returns>The serialized content.</returns>
    public static String SerializeToString<T>(T objectToString) {
      String result;
      XmlWriterSettings settings = new XmlWriterSettings();
      settings.OmitXmlDeclaration = true;
      settings.NewLineOnAttributes = true;
      settings.Indent = true;
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      XmlSerializer ser = new XmlSerializer(typeof(T));
      using (StringWriter textWriter = new StringWriter()) {
        using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings)) {
          ser.Serialize(xmlWriter, objectToString, emptyNs);
          result = textWriter.ToString();
        }
      }
      return result;
    }
  }
}
