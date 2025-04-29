using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private float startTime;
    private float stopTime;
    private float timerTime;
    private bool isRunning = false;
    public GUIManager gUIManager;
    int minutesInt;
    int secondsInt;

    // Use this for initialization
    void Start()
    {
        
    }

    public void TimerStart()
    {
        if (!isRunning)
        {

            print("START");
            isRunning = true;
            //setting starttime to 0
            if (GameMaster.initialLoad){
                startTime = 0;
                timerTime = 0;
            }
            else{
                timerTime = GameMaster.lastSavedTime;
            }
            
            gUIManager.timer.gameObject.SetActive(true);
            SetTime();            
            

        }
    }

    
    public void TimerStop()
    {
        if (isRunning)
        {
            print("STOP");
            isRunning = false;
            stopTime = timerTime;
            gUIManager.timer.gameObject.SetActive(false);
        }
    }

    public void TimerRestart()
    {
        if (!isRunning)
        {
            print("Restart");
            isRunning = true;            
            timerTime = stopTime;
            gUIManager.timer.gameObject.SetActive(true);
        }
    }
    
    public float getTime(){
        return timerTime;
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();
    }

    void SetTime()
    {
        //timerTime = stopTime + (Time.time - startTime);
        timerTime = timerTime + Time.deltaTime;
        minutesInt = (int)timerTime / 60;
        secondsInt = (int)timerTime % 60;

        if (isRunning)
        {            
            gUIManager.SetTimer(minutesInt, secondsInt);
        }
    }
    
}
