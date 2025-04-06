using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public Vector3 offset;           // Offset from the player (e.g., (0, 5, -10) to keep the camera above the player)

    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement, adjust to make it more or less smooth

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // Try to find the player object automatically
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Desired position is the player's position + the offset
            Vector3 desiredPosition = player.position + offset;

            // Keep the camera's Z position constant to avoid any issues with 2D movement
            desiredPosition.z = transform.position.z;

            // Smoothly move the camera to the desired position using Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;
        }
    }
}
