using Mirror;
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
            if (board.players.Count == NetworkServer.connections.Count)
            {
                StartGame();
            }
        }
    }
    void StartGame()
    {
        board.CreateDeck(10, true, 0);
        //board.AddCard(0, new Card("gamer", "shit"));
        board.Hand(0, 0);
    }
}
