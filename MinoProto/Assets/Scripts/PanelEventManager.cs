using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PanelEventManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject badEndPanel;
    public TimerController timerController;
    public PlayerController playerController;

    public GUIManager gUIManager;

    public GameMaster gm;

    public bool inTextGUI = false;

    public FightLoader fightLoader;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindAnyObjectByType<GameMaster>();
        gUIManager = FindAnyObjectByType<GUIManager>();
        fightLoader = FindAnyObjectByType<FightLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pause screen info

    public void RestartGameMaze()
    {
        GameMaster.initialLoad = true;
        Stats.currentHealth = Stats.maxHealth;
        SceneManager.LoadScene(0);
        //stop timer instead of full restart
        if (timerController){
            timerController.TimerStop();        

        }
        Time.timeScale = 1;
        gUIManager.inTextGUI = false;

    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        timerController.TimerRestart();
        Time.timeScale = 1;

        //Cursor locked back in middle and not visible for player control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //player can move again
        gUIManager.inTextGUI = false;

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application has quit.");
    }

    public void Retry()
    {
        Debug.Log("Retry");
        Stats.currentHealth = Stats.maxHealth;
        gm.setFalse(GameMaster.lastInteractedSkeleton);
        fightLoader.LoadNextFight(0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;


    }

    public void RestartFight()
    {
        Stats.currentHealth = Stats.maxHealth;
        fightLoader.LoadNextFight(0);
        Time.timeScale = 1;

    }

}
