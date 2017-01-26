using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

public class Level{

    [XmlAttribute("name")]
    public string name;

    // auracolor element
    [XmlElement("A_C")]
    public string A_C;

    // auracolor coe element
    [XmlElement("A_C_C")]
    public string A_C_C;

    // auracolor coe element
    [XmlElement("colorTypeCode")]
    public int colorTypeCode;

    // auracolor coe element
    [XmlElement("isBlend")]
    public bool isBlend;

    //max turns player gets
    //Easy
    [XmlElement("M_T_E")]
    public float M_T_E;
    //Medium
    [XmlElement("M_T_M")]
    public float M_T_M;
    //Hard
    [XmlElement("M_T_H")]
    public float M_T_H;

    //total aurapower in level
    //easy
    [XmlElement("A_P_E")]  
    public float A_P_E;

    //medium
    [XmlElement("A_P_M")]
    public float A_P_M;
    //Hard
    [XmlElement("A_P_H")]
    public float A_P_H;

    //Resources: money 
    [XmlElement("StartupMoney")]
    public float StartupMoney;

    //Resources: power 
    [XmlElement("StartupPower")]
    public float StartupPower;

    [XmlElement("StartupHeat")]
    public float StartupHeat;
}
