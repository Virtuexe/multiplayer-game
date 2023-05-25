using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 center;
    public Vector2 size;
    Camera cam;

    Deck _hand;
    public Deck hand
    {
        get
        {
            return _hand;
        }
        set
        {
            // Detach event from old instance (if any)
            if (_hand != null)
            {
                _hand.OnInventory -= UpdateCardsInHand;
            }

            _hand = value;

            // Attach event to new instance (if any)
            if (_hand != null)
            {
                _hand.OnInventory += UpdateCardsInHand;
            }

            cam = GetComponent<Camera>();
            UpdateViewRectangle(0.5f);
        }
    }
    public void UpdateViewRectangle(float distance)
    {
        // Calculate the dimensions of the rectangle based on the camera's field of view and aspect ratio
        size.y = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        size.x = size.y * cam.aspect;

        // Calculate the center of the rectangle based on the camera's position and orientation
        center = cam.transform.position + cam.transform.forward * distance;
    }
    private void UpdateCardsInHand()
    {
    }
}
