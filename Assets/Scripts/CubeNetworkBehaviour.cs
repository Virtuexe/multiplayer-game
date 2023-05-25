using Mirror;
using UnityEngine;

public class CubeNetworkBehaviour : NetworkBehaviour
{
    public static CubeNetworkBehaviour thisCube;
    public override void OnStartClient()
    {
        thisCube = this;
    }
    public void ChooseColor()
    {
        Color color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
        RpsChangeColor(color);
    }
    [ClientRpc]
    public void RpsChangeColor(Color color)
    {
        thisCube.GetComponent<Renderer>().material.SetColor("_Color",color);
    } 
}
