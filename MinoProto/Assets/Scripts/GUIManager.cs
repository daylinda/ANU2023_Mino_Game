using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class GUIManager : MonoBehaviour
{
    public Button startButton;

    public GameObject clickPrompt;
    public TextMeshProUGUI itemText;

    public Button riddle1Button;
    public Button riddle2Button;
    public Button riddle3Button;
    public Button guideDialogueButton;
    public Button guideDialogue2Button;
    public Button eagleDialogueButton;
    public Button healthDialogueButton;
    public Button lostGuideDialogueButton;
    public Button potionDialogueButton;
    public Button skeletonDialogueButton;


    public GameObject fightOrRiddle;

    public Button fr1;
    public Button fr2;
    public Button fr3;
    public Button fr4;
    public Button friddle1;
    public Button friddle2;
    public Button friddle3;
    public Button right;
    public Button wrong;

    public Button goodend;

    public Button fightButton;
    public Button playerDeath;
    public Button minoDeath;

    public Button minoHealth;
    public Button playerHealth;

    public GameObject eagleEyeCounter;
    public TextMeshProUGUI eagleEyeTimer;
    public TextMeshProUGUI timer;

    public RawImage eagleIcon;
    public RawImage sprintIcon;
    public Texture sprint;
    public Texture sprintPressed;
    public Texture eagle;
    public Texture eaglePressed;
    public TextMeshProUGUI eagleText;

    public RawImage attackIcon;
    public Texture attack;
    public Texture attackPressed;

    public Image progressBar;

    public GameObject pausePanel;
    public TimerController timerController;
    public PlayerController playerController;

    public bool guideDialogue1Visible = false;
    public bool guideDialogue2Visible = false;

    public bool inTextGUI = false;
    public bool introOver = false;

    public Canvas goodEndPanel;

    public FightLoader fightLoader;
    public GameMaster gm; 

    // Start is called before the first frame update
    void Start()
    {
        clickPrompt.SetActive(false);
        riddle1Button.gameObject.SetActive(false);
        riddle2Button.gameObject.SetActive(false);
        riddle3Button.gameObject.SetActive(false);
        guideDialogueButton.gameObject.SetActive(false);
        fightOrRiddle.SetActive(false);
        fightLoader = FindAnyObjectByType<FightLoader>();
        gm = FindAnyObjectByType<GameMaster>();
    }

  

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintIcon.texture = sprintPressed;
        }
        else
        {
            sprintIcon.texture = sprint;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            timerController.TimerStop();
            pausePanel.SetActive(true);
            //unlocks cursor so not stuck in middle and makes it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //so player cant move in menu
            Time.timeScale = 0;
            inTextGUI = true;
        }

    }


    public void clickPromptOn(string name)
    {
        itemText.text = name;
        clickPrompt.SetActive(true);
    }

    public void clickPromptOff()
    {
        clickPrompt.SetActive(false);
    }

    public void FinalRoomHealthPlayer(float playerHealthInt)
    {
        playerHealth.GetComponentInChildren<TextMeshProUGUI>().text = "Player: " + playerHealthInt;

        playerHealth.gameObject.SetActive(true);

    }

    public void FinalRoomHealthMino(float minoHealthInt)
    {
        minoHealth.GetComponentInChildren<TextMeshProUGUI>().text = "Minotaur: " + minoHealthInt;

        minoHealth.gameObject.SetActive(true);

    }

    public void FinalRoomHealthOff()
    {

        minoHealth.gameObject.SetActive(false);
        playerHealth.gameObject.SetActive(false);


    }

    public void FinalRoom(string name)
    {
        if(name == "FightOrRiddle")
        {
            fightOrRiddle.SetActive(true);
        }
        if (name == "1")
        {
            fr1.gameObject.SetActive(true);
            fightOrRiddle.SetActive(false);
        }
        if (name == "2")
        {
            fr2.gameObject.SetActive(true);
            fr1.gameObject.SetActive(false);
        }
        if (name == "3")
        {
            fr3.gameObject.SetActive(true);
            fr2.gameObject.SetActive(false);
        }
        if (name == "4")
        {
            fr4.gameObject.SetActive(true);
            fr3.gameObject.SetActive(false);
        }
        if (name == "riddle1")
        {
            friddle1.gameObject.SetActive(true);
            fr4.gameObject.SetActive(false);
        }
        if (name == "riddle2")
        {
            friddle2.gameObject.SetActive(true);
            right.gameObject.SetActive(false);
        }
        if (name == "riddle3")
        {
            friddle3.gameObject.SetActive(true);
            right.gameObject.SetActive(false);
        }
        if (name == "right")
        {
            right.gameObject.SetActive(true);
            friddle1.gameObject.SetActive(false);
            friddle2.gameObject.SetActive(false);

        }
        if (name == "wrong")
        {
            wrong.gameObject.SetActive(true);
            friddle1.gameObject.SetActive(false);
            friddle2.gameObject.SetActive(false);
            friddle3.gameObject.SetActive(false);
        }
        if (name == "end")
        {
            goodend.gameObject.SetActive(true);
            friddle3.gameObject.SetActive(false);
        }

        if(name == "fight")
        {
            fightButton.gameObject.SetActive(true);
            fightOrRiddle.SetActive(false);

        }
        if (name == "fightoff")
        {
            fightButton.gameObject.SetActive(false);
        }
        if (name == "wrongoff")
        {
            wrong.gameObject.SetActive(false);
        }

        if (name == "playerDeath")
        {
            playerDeath.gameObject.SetActive(true);
        }

        if (name == "minoDeath")
        {
            minoDeath.gameObject.SetActive(true);
        }

    }


    public void Riddle1()
    {
        riddle1Button.gameObject.SetActive(true);
        inTextGUI = true;

    }

    public void Riddle1Off()
    {
        riddle1Button.gameObject.SetActive(false);
        inTextGUI = false;
    }

    public void Riddle2()
    {
        riddle2Button.gameObject.SetActive(true);
        inTextGUI = true;

    }

    public void Riddle2Off()
    {
        riddle2Button.gameObject.SetActive(false);
        inTextGUI = false;

    }

    public void Riddle3()
    {
        riddle3Button.gameObject.SetActive(true);
        inTextGUI = true;
    }

    public void Riddle3Off()
    {
        riddle3Button.gameObject.SetActive(false);
        inTextGUI = false;
    }

    public void SetEagleEyeCount(int count)
    {
        eagleEyeCounter.GetComponentInChildren<TextMeshProUGUI>().text = $"{count}";
    }

    public void SetEagleEyeTimer(float time)
    {
        time = (int)time;
        eagleEyeTimer.text = time>10? $"Time Left: 00:{time }": $"Time Left: 00:0{time }";      
    }

    public void SetTimer(int minutesInt, int secondsInt)
    {
        var minutes = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
        var seconds = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();

        timer.text = minutes + ":" + seconds;
    }

    public void GuideDialogue()
    {
        guideDialogue1Visible = true;
        guideDialogueButton.gameObject.SetActive(true);
        inTextGUI = true;

    }

    public void GuideDialogueOff()
    {
        guideDialogueButton.gameObject.SetActive(false);
        inTextGUI = false;
    }

    public void GuideDialogue2()
    {
        guideDialogue2Button.gameObject.SetActive(true);
        inTextGUI = true;


    }

    public void GuideDialogue2Off()
    {
        guideDialogue2Visible = false;
        guideDialogue2Button.gameObject.SetActive(false);
        inTextGUI = false;
        //for eagle eye to show training is over
        introOver = true;
    }

    public void EagleDialogue()
    {
        eagleDialogueButton.gameObject.SetActive(true);
        inTextGUI = true;

    }

    public void EagleDialogueOff()
    {
        eagleDialogueButton.gameObject.SetActive(false);
        inTextGUI = false;

    }
    public void LostGuideDialogueOff()
    {
       lostGuideDialogueButton.gameObject.SetActive(false);
       inTextGUI = false;

    }

    public void LostGuideDialogue()
    {
        lostGuideDialogueButton.gameObject.SetActive(true);
        inTextGUI = true;

    }

    internal void PotionDialogue()
    {
        potionDialogueButton.gameObject.SetActive(true);
        inTextGUI = true;

    }

    internal void PotionDialogueOff()
    {
        potionDialogueButton.gameObject.SetActive(false);
        inTextGUI = false;

    }

    internal void SkeletonDialogue()
    {
        skeletonDialogueButton.gameObject.SetActive(true);
        inTextGUI = true;
    }

    internal void SkeletonDialogueOff()
    {
        
        skeletonDialogueButton.gameObject.SetActive(false);
        inTextGUI = false;

        Stats.lastPos = GameObject.Find("Player").transform;
        fightLoader.LoadNextFight(2);
    }

    internal void goodEnding()
    {
        GameMaster.initialLoad = true;
        goodEndPanel.gameObject.SetActive(true);
        goodend.gameObject.SetActive(false);

        timerController.TimerStop();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        inTextGUI = true;

    }
}
