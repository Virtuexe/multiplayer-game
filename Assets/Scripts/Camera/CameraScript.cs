using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour, NoteEvents
{
    public const float distance = 0.1f;
    public int deck;
    public Vector3 center;
    public Vector2 size;
    public Camera cam;
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
    public void Added()
    {
        Instantiate(Card.prefab);
    }
    public void Removed(int index)
    {

    }
    public void Inserted(int index)
    {

    }
}
