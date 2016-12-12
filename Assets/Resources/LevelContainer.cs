using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

[XmlRoot("Levelcollection")]
public class LevelContainer{

    [XmlArray("Levels")]
    [XmlArrayItem("Level")]
    public List<Level> levels = new List<Level>();

    public static LevelContainer Load(string path)
    {
        TextAsset _xml = Resources.Load <TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(LevelContainer));
        StringReader reader = new StringReader(_xml.text);

        LevelContainer levels = serializer.Deserialize(reader) as LevelContainer;

        reader.Close();

        return levels;
    }
}
