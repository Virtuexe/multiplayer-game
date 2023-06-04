using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public delegate void OnRevealHandler();
public struct Card
{
    public string name;
    public string description;
    public static GameObject prefab = Resources.Load<GameObject>("Assets/Resources/Board/Card.prefab");

    public static Card unknownCard = new Card("unknown", "unknown");
    public Card(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}
