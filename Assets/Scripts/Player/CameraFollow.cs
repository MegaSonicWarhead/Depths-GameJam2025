using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;          // Reference to the player's transform
    public Vector3 offset;           // Offset from the player (e.g., (0, 5, -10) for a 2D camera)
    public float smoothSpeed = 0.125f; // Smoothness factor for camera movement

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // Automatically find the player if not assigned
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Desired position is the player's position plus the offset
            Vector3 desiredPosition = player.position + offset;

            // Keep the camera's Z position constant (important for 2D games)
            desiredPosition.z = -10; // Ensure the camera stays in 2D space

            // Smoothly move the camera to the desired position using Lerp
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;
        }
    }
}
