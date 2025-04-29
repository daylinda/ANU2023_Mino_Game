using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Interactable : MonoBehaviour
{
    public float interactionDistance = 50.0f;
    private bool isInRange = false;

    public FlowerOrderManager flowerOrderManager;
    public GUIManager gUIManager;
    public RotatingLightManager rotatingLightManager;

    public PlayerController playerController;

    public AudioClip interactSound;
    public AudioClip puzzle2Notes;
    private AudioSource playerAudio;

    private CameraSwitch cameraSwitch;

    public DialogueManager dialogueManager;
    public HealthBarManager healthBarManager;
    private bool skeletonDialogueIsOn = false;
    private GameMaster gm;
    private Animator skeletonAnimator;
    private void Start()
    {
        flowerOrderManager = FindObjectOfType<FlowerOrderManager>();
        gUIManager = FindAnyObjectByType<GUIManager>();
        rotatingLightManager = FindAnyObjectByType<RotatingLightManager>();

        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();

        cameraSwitch = GameObject.Find("CameraController").GetComponent<CameraSwitch>();

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        healthBarManager = GameObject.Find("Canvas").GetComponentInChildren<HealthBarManager>();
        if (gameObject.CompareTag("Skeleton")){
            skeletonAnimator = GetComponent<Animator>();
        }
        gm = FindAnyObjectByType<GameMaster>();
        //dialogueManager = GameObject.Find("DialogueBox").GetComponent<DialogueManager>();
        //error here

    }


    private void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }

        CheckDialogue();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gUIManager.Riddle1Off();
            gUIManager.Riddle2Off();
            gUIManager.Riddle3Off();
            gUIManager.EagleDialogueOff();
            gUIManager.LostGuideDialogueOff();
            gUIManager.PotionDialogueOff();
            if (skeletonDialogueIsOn){
                gUIManager.SkeletonDialogueOff();
                skeletonDialogueIsOn = false;
            }

        }

    }

    private void CheckDialogue()
    {
        
        if (gUIManager.guideDialogue1Visible && Input.GetKeyDown(KeyCode.C))
        {
            gUIManager.GuideDialogueOff();
            gUIManager.guideDialogue1Visible = false;
            gUIManager.GuideDialogue2();
            gUIManager.guideDialogue2Visible = true;
        }
        if(gUIManager.guideDialogue2Visible && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            gUIManager.GuideDialogue2Off();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Skeleton"))
            {
                Debug.Log("Index"+transform.GetSiblingIndex());
                gm.skeletonsDead[transform.GetSiblingIndex()] = true;
                gm.setTrue(transform.GetSiblingIndex());
                gUIManager.SkeletonDialogue();
                skeletonAnimator.SetTrigger("approached");
                skeletonDialogueIsOn = true;
                
            }
            else{
            isInRange = true;
            gUIManager.clickPromptOn(gameObject.name);

            if (gameObject.CompareTag("Eagle"))
            {
                gUIManager.clickPromptOn("Zeus's Blessing");
            }

 

            if (gameObject.CompareTag("Potion"))
            {
                Debug.Log("Close to potion");
                gUIManager.clickPromptOn("Health Potion");
            }

            if (gameObject.CompareTag("Statue"))
            {
                gUIManager.clickPromptOn("Statue");
            }

            if (gameObject.CompareTag("Dog")|| gameObject.CompareTag("LostGuide"))
            {
                gUIManager.clickPromptOn("Guide");
            }

            if (gameObject.CompareTag("Flower") && flowerOrderManager.flowerComplete)
            {
                isInRange = false;
                gUIManager.clickPromptOff();
            }

            if (gameObject.CompareTag("RotateLight"))
            {
                
                if (rotatingLightManager.isComplete)
                {
                    isInRange = false;
                    gUIManager.clickPromptOff();
                } else
                {
                    gUIManager.clickPromptOn("Rotating Light");

                }
            }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            gUIManager.clickPromptOff();
        }
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacting with: " + gameObject.name);

        playerAudio.PlayOneShot(interactSound, 1.0f);


        if (gameObject.CompareTag("Statue"))
        {
            ShowStatueDialogue();
        }


        if (gameObject.CompareTag("Flower"))
        {
            flowerOrderManager.PickUpFlower(gameObject);
        }

        if (gameObject.CompareTag("RotateLight"))
        {
            rotatingLightManager.RotateLight(gameObject);
        }

        if (gameObject.CompareTag("Eagle"))
        {
            Destroy(gameObject);
            gUIManager.clickPrompt.SetActive(false);
            cameraSwitch.AddEagleEye();
            if (cameraSwitch.isFirstEEPickUp)
            {
                gUIManager.EagleDialogue();
                cameraSwitch.isFirstEEPickUp = false;
            }
        }

        if (gameObject.CompareTag("Dog"))
        {
            //Debug.Log("Inside compare tag dog");            
            //guideDialogue1Visible = true;
            //Debug.Log("Dialogue1 visible" + guideDialogue1Visible.ToString());
            gUIManager.clickPrompt.SetActive(false);
            gUIManager.GuideDialogue();            
            Destroy(gameObject);            
        }

        if (gameObject.CompareTag("Potion"))
        {
            if (Stats.currentHealth < Stats.maxHealth){
            Destroy(gameObject);
            Debug.Log("Inside compare tag potion");
            
            gUIManager.clickPrompt.SetActive(false);
            if (!GameMaster.potionPickedUp)
            {
                gUIManager.PotionDialogue();
                GameMaster.potionPickedUp = true;
            }
            Stats.currentHealth++;
            healthBarManager.setHealth();
            }

        }

        if (gameObject.CompareTag("LostGuide"))
        {
            gUIManager.clickPrompt.SetActive(false);
            gUIManager.LostGuideDialogue();
        }


    }

    private void ShowStatueDialogue()
    {
        if(gameObject.name == "Statue1")
        {
            gUIManager.Riddle1();
        }

        if (gameObject.name == "Statue2")
        {
            gUIManager.Riddle2();
            playerAudio.PlayOneShot(puzzle2Notes, 1.0f);

        }

        if (gameObject.name == "Statue3")
        {
            gUIManager.Riddle3();
        }
    }
}
 
