using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Reference to the player's Transform
    public float smoothSpeed = 1; // Smoothness of the camera movement
    public Vector3 offset;            // Offset to adjust the camera's position relative to the player

    void Start()
    {
        // Check if the player has been assigned in the inspector
        if (player == null)
        {
            Debug.LogError("Player not assigned in CameraFollow script!");
        }
    }

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        if (player != null)
        {
            // Define the desired position of the camera with the offset
            Vector3 desiredPosition = player.position + offset;

            // Smoothly move the camera towards the desired position using Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;
        }
    }
}
