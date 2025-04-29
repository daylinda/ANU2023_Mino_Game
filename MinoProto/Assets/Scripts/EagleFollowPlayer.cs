using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFollowPlayer : MonoBehaviour
{
    public Transform camTarget; // The player's transform
    public float followDistance = 2.0f; // Distance from the player
    public float followHeight = 689.0f; // Height above the player
    public float smoothSpeed = 0.2f; // Smoothing factor

    private void Update()
    {
        // Calculate the desired camera position
        Vector3 targetPosition = new Vector3(camTarget.position.x,followHeight,camTarget.position.z);

        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
               
    }
}
