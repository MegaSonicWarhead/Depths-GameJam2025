using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Smoothness of the camera movement
    public Vector3 offset;            // Offset to adjust the camera's position relative to the player

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            // Define the desired position of the camera with the offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the new position to the camera
            transform.position = smoothedPosition;
        }
    }
}
