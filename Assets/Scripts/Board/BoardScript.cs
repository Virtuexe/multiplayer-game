using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : NetworkBehaviour
{
    public const int deck_max = 256;
    public Card[] cards;
    public Note<Deck> decks = new Note<Deck>(deck_max);
    public DeckEvents[] deckObjects;
    public int[,] cards_index;

    public void Awake()
    {
        cards_index = new int[deck_max, cards.Length];
    }

    public List<NetworkConnection> players = new List<NetworkConnection>();
    //Deck : create
    public int CreateDeck(bool hidden, int playerExceptions)
    {
        isReady = false;
        decks.Add(new Deck(hidden, playerExceptions));
        foreach (NetworkConnection player in players)
            TargetCreateDeck(player, hidden, playerExceptions);
        SetReady();
        return decks.CurrentLength - 1;
    }
    public int CreateDeck()
    {
        isReady = false;
        decks.Add(new Deck(false, 1));
        foreach (NetworkConnection player in players)
            TargetCreateDeck(player, false, 0);
        SetReady();
        return decks.CurrentLength - 1;
    }

    [TargetRpc]
    public void TargetCreateDeck(NetworkConnection target, bool hidden, int playerExceptions)
    {
        if (isServer) return;
        decks.Add(new Deck(hidden, playerExceptions));
    }
    //DECK : shuffle
    int temp;
    int rand;
    
    public void ShuffleDeck(int deck)
    {
        for (int i = 0; i < decks[deck].currentLength; i++)
        {
            temp = cards_index[decks[deck].index, i];
            rand = Random.Range(0, decks[deck].currentLength);
            cards_index[decks[deck].index, i] = cards_index[decks[deck].index, rand];
            cards_index[decks[deck].index, rand] = temp;
        }
    }
    //CARD : move
    public void MoveCard(int fromDeck, int index, int toDeck)
    {
        if (decks[fromDeck].currentLength > index && decks[toDeck].maxLength > index)
        {
            AddCard(toDeck, cards_index[fromDeck,index]);
            RemoveCard(fromDeck, index);
        }
    }
    //CARD : add
    public void AddCard(int deck, int card)
    {
        isReady = false;
        decks[deck].Add(card);
        for (int i = 0; i < players.Count; i++)
        {
            if (!decks[deck].hidden || decks[deck].playerException == i)
            {
                TargetAddCard(players[i], deck, card);
            }
            else
            {
                TargetAddCard(players[i], deck);
            }
        }
        SetReady();
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int deck, int card)
    {
        if (isServer) return;
        decks[deck].Add(card);
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int deck)
    {
        if (isServer) return;
        decks[deck].Add(0);
    }
    //CARD : remove
    public void RemoveCard(int deck, int index)
    {
        isReady = false;
        decks[deck].Remove(index);
        foreach (NetworkConnection player in players)
            TargetRemoveCard(player, deck, index);
        SetReady();
    }
    [TargetRpc]
    private void TargetRemoveCard(NetworkConnection target, int deck, int card)
    {
        if (isServer) return;
        decks[deck].Remove(card);
    }
    //DECK OBJECT : subscribe
    public void SubscribeDeckObject(int player, int deckObjeckt, int deck)
    {
        isReady = false;
        TargetSubscribeDeckObject(players[player], deckObjeckt, deck);

    }
    [TargetRpc]
    private void TargetSubscribeDeckObject(NetworkConnection target, int deckObjeckt, int deck)
    {
        decks[deck].events = deckObjects[deckObjeckt];
        decks[deck].events.deck = deck;
        if (decks[deck].events != null) Debug.Log("subscribed deck object");
        SetReady();
    }
    public bool isReady;

    [Command(requiresAuthority = false)]
    void SetReady()
    {
        isReady = true;
    }
}