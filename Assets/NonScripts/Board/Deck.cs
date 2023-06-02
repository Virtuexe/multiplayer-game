using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public struct Deck
{
    public bool hidden;
    public int playerException;

    public int selectedCard;

    public int cardAmount;
    public Note<int> cards_index;
    public Deck(bool hidden, int playerException)
    {
        this.cardAmount = 0;
        this.selectedCard = 0;
        this.cards_index = new Note<int>(BoardScript.card_max);
        this.hidden = hidden;
        this.playerException = playerException;
    }
}
