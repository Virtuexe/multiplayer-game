using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public int deck;
    public Vector3 center;
    public Vector2 size;
    public Camera cam;
    public void UpdateViewRectangle(float distance)
    {
        // Calculate the dimensions of the rectangle based on the camera's field of view and aspect ratio
        size.y = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        size.x = size.y * cam.aspect;

        // Calculate the center of the rectangle based on the camera's position and orientation
        center = cam.transform.position + cam.transform.forward * distance;
    }
    private void AddCard(int index)
    {

    }
    private void RemoveCard(int index)
    {
    }
}
