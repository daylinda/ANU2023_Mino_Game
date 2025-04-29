using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MonoBehaviour
{

    public FinalRoomManager finalRoomManager;

    private Rigidbody enemyRb;   // Reference to the Rigidbody component of the enemy
    private GameObject player;   // Reference to the player GameObject
    public float speed;          // Speed at which the enemy moves

    public float health = 100;

    private Animator minoAnim;

    public GUIManager gUIManager;



    // Start is called before the first frame update
    void Start()
    {
        finalRoomManager = FindAnyObjectByType<FinalRoomManager>();

        // Get the Rigidbody component attached to this enemy GameObject
        enemyRb = GetComponent<Rigidbody>();

        // Find the player GameObject with the name "Player" and store a reference to it
        player = GameObject.Find("Player");

        minoAnim = GetComponent<Animator>();
        gUIManager = FindAnyObjectByType<GUIManager>();


    }

    // Update is called once per frame
    void Update()
    {

        if(finalRoomManager.attackStarted == true)
        {
            gUIManager.FinalRoomHealthMino(health);

            // Calculate the direction from the enemy to the player and normalize it
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;

            // Calculate the rotation that points in the player's direction
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Smoothly rotate the object towards the player's direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);

            // Apply a force to the enemy Rigidbody in the calculated direction with the specified speed
            enemyRb.AddForce(lookDirection * speed);
            minoAnim.SetBool("Run_b", true);
        }

        if (health <= 0)
        {
            minoAnim.SetBool("Run_b", false);
            minoAnim.SetBool("Dead_b", true);
            gUIManager.FinalRoomHealthOff();



        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Debug.Log("HIT" + other);
            health -= 10;
            Destroy(other.gameObject);
        }
    }
}
