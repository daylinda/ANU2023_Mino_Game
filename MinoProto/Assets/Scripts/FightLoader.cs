using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FightLoader : MonoBehaviour
{
    public Animator transition;
    private GameMaster gm;
    private PlayerController player;
    private TimerController timerController;

    // Update is called once per frame
    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        player = FindAnyObjectByType<PlayerController>();
        timerController = FindAnyObjectByType<TimerController>();
    }
     

    
    public void LoadNextFight(int levelIndex){
        if (timerController){
            GameMaster.lastSavedTime = timerController.getTime();
            timerController.TimerStop();
        }
        if (player){
            GameMaster.initialLoad = false;
            gm.lasterPlayerPos = player.transform.position;
            gm.lastPlayerRot = player.transform.rotation;
        }
        StartCoroutine(LoadFight(levelIndex));
    }

    IEnumerator LoadFight(int levelIndex){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelIndex);
    }
}
