using Mirror;
using System;
using System.Collections;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;


    public BoardScript board;

    void Awake()
    {
        Instance = this;
    }

    public void PlayerReady(NetworkConnection player)
    {
        if (!board.players.Contains(player))
        {
            board.players.Add(player);
            if (board.players.Count == 2)
            {
                StartCoroutine(StartGame());
            }
        }
    }
    int mainDeck;
    int discardDeck;
    int playerDeckStart;
    int playerTurn;

    IEnumerator StartGame()
    {
        Debug.Log("game started");
        //create main deck
        mainDeck = board.CreateDeck(true, -1);
        yield return new WaitUntil(() => board.isReady);
        for (int i = 1; i < board.cards.Length; i++)
        {
            board.decks[mainDeck].Add(i);
        }
        yield return new WaitUntil(() => board.isReady);
        board.ShuffleDeck(mainDeck);
        //create hands
        playerDeckStart = board.decks.CurrentLength;
        for (int i = 0; i < board.players.Count; i++)
        {
            board.CreateDeck(true, i);
            yield return new WaitUntil(() => board.isReady);
            board.SubscribeDeckObject(i, 0, playerDeckStart + i);
            yield return new WaitUntil(() => board.isReady);
            Debug.Log("creating player");
            // Wait until the events are not null
            

            for (int j = 0; j < 4; j++)
            {
                Debug.Log("moving");
                board.MoveCard(mainDeck, board.decks[mainDeck].currentLength - 1, playerDeckStart + i);
            }
        }
        //discard deck
        discardDeck = board.CreateDeck();
        Debug.Log("waiting");
        yield return new WaitUntil(() => board.isReady);
        Debug.Log("stopped waiting");
        for (int i = 0; i < board.players.Count; i++)
        {
            board.SubscribeDeckObject(i, 1, discardDeck);
        }
        yield return new WaitUntil(() => board.isReady);
        board.MoveCard(mainDeck, board.decks[mainDeck].currentLength - 1, discardDeck);

        //player
        playerTurn = 0;
        Debug.Log("end of start");
    }
    public IEnumerator PlayerPlayCard(NetworkConnection player, int index)
    {
        if (board.players.FindIndex(p => p == player) == playerTurn)
        {
            if (board.cards[board.cards_index[playerDeckStart + playerTurn, index]].type == board.cards[board.cards_index[discardDeck, board.decks[discardDeck].currentLength - 1]].type || board.cards[board.cards_index[playerDeckStart + playerTurn, index]].value == board.cards[board.cards_index[discardDeck, board.decks[discardDeck].currentLength - 1]].value)
                board.MoveCard(playerDeckStart + playerTurn, index, discardDeck);
            yield return new WaitUntil(() => board.isReady);
        }
        if (playerTurn + 1 < board.players.Count)
            playerTurn++;
        else
            playerTurn = 0;
    }
    public IEnumerator PlayerDrawCard(NetworkConnection player, int index)
    {
        if (board.players.FindIndex(p => p == player) == playerTurn)
        {
            board.MoveCard(mainDeck, index, playerDeckStart + playerTurn);
            yield return new WaitUntil(() => board.isReady);
        }
        if (playerTurn + 1 < board.players.Count)
            playerTurn++;
        else
            playerTurn = 0;
    }
}
