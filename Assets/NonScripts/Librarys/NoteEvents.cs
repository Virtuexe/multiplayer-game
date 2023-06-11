using System;
using UnityEngine;

public abstract class DeckEvents : MonoBehaviour
{
    public abstract int deck { get; set; }
    public abstract void CardAdded(int card);
    public abstract void CardInserted(int index, int card);
    public abstract void CardRemoved(int index);
}
