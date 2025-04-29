using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform camTarget; // The player's transform
    public float followDistance = 2.0f; // Distance from the player
    public float followHeight = 2.0f; // Height above the player
    private float smoothSpeed = 0.2f; // Smoothing factor

    private void LateUpdate()
    {
        // Calculate the desired camera position
        Vector3 targetPosition = camTarget.position + camTarget.forward * -followDistance + camTarget.up * followHeight;

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Look at the player's position
        transform.LookAt(camTarget);
    }
}
