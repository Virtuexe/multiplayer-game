using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public delegate void OnRevealHandler();
public struct Card
{
    public string name;
    public string description;
    public Sprite sprite;
    public GameObject gameObject;

    public event Action OnReveal;

    public static Card hidden = new Card("unknown", "unknown", null, null);
    public static GameObject prefab = Resources.Load<GameObject>("Board/Card");

    public Card(string name, string description, Sprite sprite, Action OnReveal)
    {
        this.name = name;
        this.description = description;
        this.sprite = sprite;

        this.gameObject = null;

        this.OnReveal = OnReveal;
        InstantiateCard();
    }
    public void InstantiateCard()
    {
        gameObject = GameObject.Instantiate(prefab);
    }
}
