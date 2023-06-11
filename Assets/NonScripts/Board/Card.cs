using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

[Serializable]
public struct Card
{
    public static GameObject prefab = Resources.Load<GameObject>("Board/Card");
    public Sprite sprite;
    public int value;
    public Type type;
    public enum Type
    {
        blue,
        green,
        red,
        yellow
    }
}
