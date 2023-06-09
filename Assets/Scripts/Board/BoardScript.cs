using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : NetworkBehaviour
{
    public const int card_max = 256;
    public const int deck_max = 256;
    public Note<Deck> decks = new Note<Deck>(deck_max);
    public Note<DeckEvents> deckObjects = new Note<DeckEvents>(deck_max);
    public Note<Card> cards = new Note<Card>(card_max);
    public int[,] cards_index = new int[deck_max,card_max];

    public List<NetworkConnection> players = new List<NetworkConnection>();
    //Deck : create
    public int CreateDeck(bool hidden, int playerExceptions, int noteObject)
    {
        decks.Add(new Deck(hidden, playerExceptions, noteObject));
        RpcCreateDeck(hidden, playerExceptions, noteObject);
        return decks.CurrentLength - 1;
    }

    [ClientRpc]
    public void RpcCreateDeck(bool hidden, int playerExceptions, int noteObject)
    {
        decks.Add(new Deck(hidden, playerExceptions, noteObject));
    }
    //DECK : shuffle
    int temp;
    int rand;
    public void ShuffleDeck(int deck)
    {
        for(int i = 0; i < decks[deck].currentLength; i++)
        {
            temp = cards_index[decks[deck].index, i];
            rand = Random.Range(0, decks[deck].currentLength);
            cards_index[decks[deck].index,i] = cards_index[decks[deck].index, rand];
            cards_index[decks[deck].index, rand] = temp;
        }
    }
    //CARD : move
    public void MoveCard(int fromDeck, int card, int toDeck)
    {
        if (decks[fromDeck].currentLength > card && decks[toDeck].maxLength > card)
        {
            AddCard(toDeck, card);
            RemoveCard(fromDeck, card);
        }
    }
    //CARD : add
    public void AddCard(int deck, int card)
    {
        decks[deck].Add(card);
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
        cards.Add(card);
        decks[deck].Add(cards.CurrentLength-1);
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int deck)
    {
        cards.Add(Card.unknownCard);
        decks[deck].Add(cards.CurrentLength - 1);
    }
    //CARD : remove
    public void RemoveCard(int deck, int card)
    {
        decks[deck].Remove(card);
        RpcRemoveCard(deck, card);
    }
    [ClientRpc]
    private void RpcRemoveCard(int deck, int card)
    {
        decks[deck].Remove(card);
    }
    //DECK : show hand
    public void Hand(int player, int deck)
    {
        TargetHand(players[player], deck);
    }
    [TargetRpc]
    public void TargetHand(NetworkConnection target, int deck)
    {
        Camera.main.GetComponent<CameraScript>().deckObject = deck;
    }
}