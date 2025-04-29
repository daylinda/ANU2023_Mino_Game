using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    private float speed = 50.0f;
    private GameObject player;   // Reference to the player GameObject
    public Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        lookDirection = player.transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile in the calculated direction
        transform.Translate(lookDirection * Time.deltaTime * speed);
    }
}
