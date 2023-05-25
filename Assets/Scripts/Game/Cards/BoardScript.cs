using Mirror;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BoardScript : NetworkBehaviour
{
    public List<Deck> decks { get; private set; } = new List<Deck>();
    public List<NetworkConnection> players = new List<NetworkConnection>();
    //Inventory
    public int CreateDeck(int size, bool hidden, int playerExceptions)
    {
        decks.Add(new Deck(size, hidden, playerExceptions));
        RpcCreateDeck(size, hidden, playerExceptions);
        return decks.Count-1;
    }

    [ClientRpc]
    public void RpcCreateDeck(int size, bool hidden, int playerExceptions)
    {
        decks.Add(new Deck(size, hidden, playerExceptions));
    }
    //CARD : move
    public void MoveCard(int fromInventory, int card, int toInventory)
    {
        if (decks[fromInventory].TryRemoveCard(card) && decks[toInventory].TryAddCard())
        {
            Card actualCard = decks[fromInventory].cards[card];
            decks[toInventory].AddCard(actualCard);
            decks[fromInventory].RemoveCard(card);
            RpcMoveCard(fromInventory, card, toInventory);
        }
    }

    [ClientRpc]
    private void RpcMoveCard(int fromInventory, int card, int toInventory)
    {
        decks[toInventory].AddCard(decks[fromInventory].cards[card]);
        decks[fromInventory].RemoveCard(card);
    }
    //CARD : add
    public void AddCard(int inventory, Card card)
    {
        if(decks[inventory].TryAddCard())
        {
            decks[inventory].AddCard(card);
            for (int i = 0; i < players.Count; i++)
            {
                if (decks[inventory].hidden || decks[inventory].playerException == i)
                {
                    TargetAddCard(players[i], inventory, card);
                }
                else
                {
                    TargetAddCard(players[i], inventory, Card.hidden);
                }
            }
        }
    }
    [TargetRpc]
    private void TargetAddCard(NetworkConnection target, int inventory, Card card)
    {
        decks[inventory].AddCard(card);
    }
    //CARD : remove
    public void RemoveCard(int inventory, int card)
    {
        if (decks[inventory].TryRemoveCard(card))
        {
            decks[inventory].RemoveCard(card);
            RpcRemoveCard(inventory, card);
        }
    }
    [ClientRpc]
    private void RpcRemoveCard(int Inventory, int card)
    {
        decks[Inventory].RemoveCard(card);
    }
    //DECK : show hand
    public void Hand(int player, int deck)
    {
        TargetHand(players[player], deck);
    }
    [TargetRpc]
    public void TargetHand(NetworkConnection target, int deck)
    {
        Camera.main.GetComponent<CameraScript>().hand = decks[deck];
    }
}