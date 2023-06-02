using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : NetworkBehaviour
{
    public const int card_max = 256;
    public const int deck_max = 256;
    public int decks_currentLength;
    public Deck[] decks = new Deck[deck_max];
    public int cards_currentLength;
    public Card[] cards = new Card[card_max];
    public List<NetworkConnection> players = new List<NetworkConnection>();
    //Deck : create
    public int CreateDeck(int size, bool hidden, int playerExceptions)
    {
        decks[decks_currentLength] = new Deck(hidden, playerExceptions);
        decks_currentLength++;
        RpcCreateDeck(hidden, playerExceptions);
        return decks_currentLength - 1;
    }

    [ClientRpc]
    public void RpcCreateDeck(bool hidden, int playerExceptions)
    {
        decks[decks_currentLength] = new Deck(hidden, playerExceptions);
    }
    //DECK : shuffle
    int temp;
    int rand;
    public void ShuffleDeck(int deck)
    {
        for(int i = 0; i < decks[deck].cards_index.CurrentLength; i++)
        {
            temp = decks[deck].cards_index[i];
            rand = Random.Range(0, decks[deck].cards_index.CurrentLength);
            decks[deck].cards_index[i] = decks[deck].cards_index[rand];
            decks[deck].cards_index[rand] = decks[deck].cards_index[i];
        }
    }
    //CARD : move
    public void MoveCard(int fromDeck, int card, int toDeck)
    {
        if (decks[fromDeck].cards_index.CurrentLength > card && decks[toDeck].cards_index.MaxLength > card)
        {
            AddCard(toDeck, card);
            RemoveCard(fromDeck, card);
        }
    }
    //CARD : add
    public void AddCard(int deck, int card)
    {
        decks[deck].cards_index.Add(card);
        for (int i = 0; i < players.Count; i++)
        {
            if (decks[deck].hidden || decks[deck].playerException == i)
            {
                TargetAddCard(players[i], deck, cards[card]);
            }
            else
            {
                TargetAddCard(players[i], deck);
            }
        }
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int deck, Card card)
    {
        cards[cards_currentLength] = card;
        decks[deck].cards_index.Add(cards_currentLength);
        cards_currentLength++;
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int deck)
    {
        cards[cards_currentLength] = Card.unknownCard;
        decks[deck].cards_index.Add(cards_currentLength);
        cards_currentLength++;
    }
    //CARD : remove
    public void RemoveCard(int deck, int card)
    {
        decks[deck].cards_index.Remove(card);
        RpcRemoveCard(deck, card);
    }
    [ClientRpc]
    private void RpcRemoveCard(int deck, int card)
    {
        decks[deck].cards_index.Remove(card);
    }
    //DECK : show hand
    public void Hand(int player, int deck)
    {
        TargetHand(players[player], deck);
    }
    [TargetRpc]
    public void TargetHand(NetworkConnection target, int deck)
    {
        Camera.main.GetComponent<CameraScript>().deck = deck;
    }
}