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
    public Deck(bool hidden, int playerException, int deckObject)
    {
        this.index = GameManagerScript.Instance.board.decks.CurrentLength - 1;
        this.cardAmount = 0;
        this.selectedCard = 0;
        this.currentLength = 0;
        this.maxLength = BoardScript.card_max;
        this.events = GameManagerScript.Instance.board.deckObjects[deckObject];
        this.hidden = hidden;
        this.playerException = playerException;
    }
    public void Add(int card)
    {
        if (currentLength >= GameManagerScript.Instance.board.decks.MaxLength)
            Console.Error.WriteLine("card cannot be added out of space");
        GameManagerScript.Instance.board.cards_index[index, currentLength] = card;
        currentLength++;
        events?.CardAdded();
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
            Console.Error.WriteLine("index is bigger than current length");
        for (int i = index; i + 1 < currentLength; i++)
        {
            GameManagerScript.Instance.board.cards_index[this.index, i + 1] = GameManagerScript.Instance.board.cards_index[this.index,i];
        }
        GameManagerScript.Instance.board.cards_index[this.index,index] = card;
        currentLength++;
        events?.CardInserted(index);
        return;
    }
}
