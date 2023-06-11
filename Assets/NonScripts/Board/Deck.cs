using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public struct Deck
{
    public int index;

    public bool hidden;
    public int playerException;

    public int selectedCard;
    public int cardAmount;
    public int currentLength;
    public int maxLength;
    public DeckEvents events;
    public Deck(bool hidden, int playerException)
    {
        this.index = GameManagerScript.Instance.board.decks.CurrentLength;
        this.cardAmount = 0;
        this.selectedCard = 0;
        this.currentLength = 0;
        this.maxLength = GameManagerScript.Instance.board.cards.Length;
        //this.events = GameManagerScript.Instance.board.deckObjects[deckObject];
        this.events = null;
        this.hidden = hidden;
        this.playerException = playerException;
    }
    public void Add(int card)
    {
        if (currentLength >= GameManagerScript.Instance.board.decks.MaxLength)
        {
            Debug.LogError("card cannot be added out of space");
            return;
        }
        GameManagerScript.Instance.board.cards_index[index, currentLength] = card;
        currentLength++;
        events?.CardAdded(card);
        return;
    }
    //Remove
    public void Remove(int index)
    {
        if (index >= currentLength)
            Console.Error.WriteLine("index is bigger than current length");
        for (int i = index; i + 1 < currentLength; i++)
        {
            GameManagerScript.Instance.board.cards_index[this.index,i] = GameManagerScript.Instance.board.cards_index[this.index,i + 1];
        }
        currentLength--;
        events?.CardRemoved(index);
        return;
    }
    //Insert
    public void Insert(int card, int index)
    {
        if (index >= currentLength)
            Debug.LogError("index is bigger than current length");
        for (int i = index; i + 1 < currentLength; i++)
        {
            GameManagerScript.Instance.board.cards_index[this.index, i + 1] = GameManagerScript.Instance.board.cards_index[this.index,i];
        }
        GameManagerScript.Instance.board.cards_index[this.index,index] = card;
        currentLength++;
        events?.CardInserted(index, card);
        return;
    }
}
