using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraScript : DeckEvents
{
    public const float distance = 2f;
    public Vector3 center;
    public Vector2 size;
    public Camera cam;
    public List<GameObject> gameObjects = new List<GameObject>();
    int _deck;
    public override int deck
    {
        get
        {
            return _deck;
        }
        set
        {
            _deck = value;
        }
    }
    private void Start()
    {
        UpdateViewRectangle(distance);
    }
    public void UpdateViewRectangle(float distance)
    {
        // Calculate the dimensions of the rectangle based on the camera's field of view and aspect ratio
        size.y = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        size.x = size.y * cam.aspect;

        // Calculate the center of the rectangle based on the camera's position and orientation
        center = cam.transform.position + cam.transform.forward * distance;
    }
    public override void CardAdded(int card)
    {
        gameObjects.Add(Instantiate(Card.prefab));
        gameObjects[gameObjects.Count - 1].GetComponent<SpriteRenderer>().sprite = GameManagerScript.Instance.board.cards[card].sprite;
        CardPositionUpdate();
    }
    public override void CardRemoved(int index)
    {
        Destroy(gameObjects[index]);
        gameObjects.RemoveAt(index);
        CardPositionUpdate();
    }
    public override void CardInserted(int index, int card)
    {
        gameObjects.Insert(index,Instantiate(Card.prefab));
        gameObjects[gameObjects.Count - 1].GetComponent<SpriteRenderer>().sprite = GameManagerScript.Instance.board.cards[card].sprite;
        CardPositionUpdate();
    }
    public void CardPositionUpdate()
    {
        Debug.Log("update");
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.position = center;
            gameObjects[i].transform.LookAt(cam.transform);
            gameObjects[i].transform.localPosition += new Vector3(i*0.5f,0, 0) - new Vector3(size.x/2 - 0.5f, size.y / 2 - 0.5f, 0);
            gameObjects[i].transform.Rotate(0, 180, 0);
        }
    }
}
