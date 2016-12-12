using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

public class Level{

    [XmlAttribute("name")]
    public string name;

    //max turns player gets
    [XmlElement("Maximumturns")]
    public float maximumTurns;

    //total aurapower in level
    [XmlElement("Auratotalpower")]  
    public float auraTotalPower;

}
