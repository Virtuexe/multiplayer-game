using Mirror;
using System;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
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
}