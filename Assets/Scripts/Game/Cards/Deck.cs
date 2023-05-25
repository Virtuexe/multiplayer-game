using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Deck
{
    public Card[] cards;
    int cardAmount;

    public bool hidden;
    public int playerException;

    public event Action OnInventory;
    public Deck(int size, bool hidden, int playerException)
    {
        this.cards = new Card[size];
        this.cardAmount = 0;
        this.OnInventory = null;
        this.hidden = hidden;
        this.playerException = playerException;
    }
    public Deck()
    {
    }
    //Add
    public void AddCard(Card card)
    {
        if (!TryAddCard())
            return;
        cards[cardAmount] = card;
        OnInventory?.Invoke();
        cardAmount++;
        return;
    }
    public bool TryAddCard()
    {
        if (cardAmount >= cards.Length)
            return false;
        return true;
    }
    //Remove
    public void RemoveCard(int index)
    {
        if (!TryRemoveCard(index))
            return;
        for (int i = index; i + 1 < cardAmount; i++)
        {
            cards[i] = cards[i + 1];
        }
        cardAmount--;
        OnInventory?.Invoke();
        return;
    }
    public bool TryRemoveCard(int index)
    {
        if (index >= cardAmount || index >= cards.Length)
        {
            Console.Error.WriteLine("Trying to access value out of bounds");
            return false;
        }
        return true;
    }
    //Insert
    public void InsertCard(Card card, int index)
    {
        if (!TryInsertCard(index))
            return;
        for (int i = index; i + 1 < cardAmount; i++)
        {
            cards[index + 1] = cards[index];
        }
        cards[index] = card;
        cardAmount++;
        OnInventory?.Invoke();
        return;
    }
    public bool TryInsertCard(int index)
    {
        if (index >= cardAmount || index >= cards.Length)
        {
            Console.Error.WriteLine("Trying to access value out of bounds");
            return false;
        }
        if (cardAmount >= cards.Length)
            return false;
        return true;
    }
}
