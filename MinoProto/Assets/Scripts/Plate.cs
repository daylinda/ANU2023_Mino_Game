using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{

    private PressurePlateManager pressurePlateManager;
    private Renderer plateRenderer;
    private AudioSource playerAudio;
    public AudioClip noteSound;



    // Start is called before the first frame update
    void Start()
    {
        pressurePlateManager = FindObjectOfType<PressurePlateManager>();
        plateRenderer = GetComponent<Renderer>();

        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Test");
            playerAudio.PlayOneShot(noteSound, 1.0f);
            pressurePlateManager.pressPlate(gameObject, plateRenderer);

        }
    }
}
