﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Xml;

public class Level{

    [XmlAttribute("name")]
    public string name;

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

}
