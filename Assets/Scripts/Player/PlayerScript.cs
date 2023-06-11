using Mirror;
using System;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    int selectedIndex = 0;
    public int player;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CmdPlayerReady();
    }
    [Command]
    void CmdPlayerReady()
    {
        GameManagerScript.Instance.PlayerReady(connectionToClient);
    }
    private void Update()
    {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedIndex > 0)
                selectedIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectedIndex < GameManagerScript.Instance.board.decks[Camera.main.GetComponent<CameraScript>().deck].currentLength - 1)
                selectedIndex++;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayCard(selectedIndex);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard(selectedIndex);
        }

    }
    [Command]
    private void PlayCard(int selectedIndex)
    {
        Debug.Log("trying to play: " + selectedIndex);
        StartCoroutine(GameManagerScript.Instance.PlayerPlayCard(connectionToClient, selectedIndex));
    }
    [Command]
    private void DrawCard(int selectedIndex)
    {
        Debug.Log("trying to draw");
        StartCoroutine(GameManagerScript.Instance.PlayerDrawCard(connectionToClient, selectedIndex));
    }
}