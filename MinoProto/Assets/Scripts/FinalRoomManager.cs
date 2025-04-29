using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalRoomManager : MonoBehaviour
{
    public bool inText = false;
    public bool inFinalRoom = false;
    public bool riddleChosen = false;
    public bool inRiddles = false;

    public int riddlePoint = 1;
    public int riddleNum = 1;


    public bool attackChosen = false;
    public bool attackStarted = false;
    public GameObject backDoor;

    public bool gameOver = false;

    public GUIManager gUIManager;

    public MinotaurController minotaurController;
    public PlayerController playerController;

    public AudioClip winSound;
    private AudioSource playerAudio;
    private FightLoader fightLoader;

    // Start is called before the first frame update
    void Start()
    {
        gUIManager = FindAnyObjectByType<GUIManager>();
        minotaurController = FindAnyObjectByType<MinotaurController>();
        playerController = FindAnyObjectByType<PlayerController>();

        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        fightLoader = FindAnyObjectByType<FightLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        if(inText && inFinalRoom  && Input.GetKeyDown(KeyCode.D) && !riddleChosen && !attackChosen)
        {
            riddleChosen = true;
            gUIManager.FinalRoom("1");
            riddlePoint = 2;
        }
        if(riddleChosen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            RiddleDialogue();
        }
        if (inRiddles)
        {
            InRiddleDialogue();
        }

        if (inText && inFinalRoom && Input.GetKeyDown(KeyCode.A) && !riddleChosen && !attackChosen)
        {
            attackChosen = true;
            AttackBegin();

        }
        if (attackChosen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            gUIManager.FinalRoom("fightoff");
            AttackBegin();

        }

        if (gameOver && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            gUIManager.goodEnding();
        }

        // if(playerController.health <= 0)
        // {
        //     gUIManager.FinalRoom("playerDeath");
        //     gameOver = true;
        //     inText = true;
        //     attackStarted = false;

        // }

        // if (minotaurController.health <= 0)
        // {
        //     gUIManager.FinalRoom("minoDeath");
        //     playerAudio.PlayOneShot(winSound, 1.0f);
        //     gameOver = true;
        //     inText = true;
        //     attackStarted = false;

        // }

        if (gameOver && (Input.GetKeyDown(KeyCode.R)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!riddleChosen && !attackChosen))
        {
            inText = true;
            inFinalRoom = true;
            gUIManager.FinalRoom("FightOrRiddle");
        }
    }
    private void AttackBegin()
    {

        backDoor.SetActive(true);
        inText = false;

        fightLoader.LoadNextFight(1);

    }

    private void RiddleDialogue()
    {
        Debug.Log(riddlePoint + " " + riddleNum);
        
        if (riddlePoint == 2)
        {
            gUIManager.FinalRoom("2");
            riddlePoint = 3;
        }
        else if (riddlePoint == 3){
            gUIManager.FinalRoom("3");
            riddlePoint = 4;
        }
        else if (riddlePoint == 4)
        {
            gUIManager.FinalRoom("4");
            riddlePoint = 5;
        }
        else if (riddlePoint == 5)
        {
            gUIManager.FinalRoom("riddle1");
            riddlePoint = 6;
            inRiddles = true;
        }
        else if (riddlePoint == 6 && riddleNum == 2)
        {
            gUIManager.FinalRoom("riddle2");
            inRiddles = true;
        }
        else if (riddlePoint == 7 && riddleNum == 3)
        {
            gUIManager.FinalRoom("riddle3");
            inRiddles = true;
        }
        else if (riddleNum == 20)
        {
            gUIManager.FinalRoom("wrongoff");
            AttackBegin();
        }

    }

    private void InRiddleDialogue()
    {
        if (riddleNum == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                gUIManager.FinalRoom("right");
                riddleNum = 2;
                inRiddles = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                gUIManager.FinalRoom("wrong");
                inRiddles = false;
                riddleNum = 20;
            }
        }

        else if (riddleNum == 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                gUIManager.FinalRoom("right");
                riddleNum = 3;
                riddlePoint = 7;
                inRiddles = false;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                gUIManager.FinalRoom("wrong");
                riddleNum = 20;
                inRiddles = false;
            }
        }

        else if (riddleNum == 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                gUIManager.FinalRoom("end");
                riddlePoint = 10;
                inRiddles = false;
                gameOver = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                gUIManager.FinalRoom("wrong");
                inRiddles = false;
                riddleNum = 20;

            }
        }
    }
}
