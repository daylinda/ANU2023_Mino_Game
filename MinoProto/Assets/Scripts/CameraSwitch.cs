using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject topCamera;
    public int allowSwitchCount = 3;
    private GUIManager gUIManager;
    public bool inTopView = false;
    public GameObject playerIndicator;
    private float timeTopView = 10.0f;
    private float timeLeft = 10.0f;
    private TimerController timerController;
    private EEProgressBar progressBar;
    public GameObject arrowIndicator;
    public bool isFirstEEPickUp = true;
    // Start is called before the first frame update
    void Start()
    {
        gUIManager = GameObject.Find("GUIManager").GetComponent<GUIManager>();
        timerController = GameObject.Find("TimerController").GetComponent<TimerController>();
        progressBar = GameObject.Find("CameraController").GetComponent<EEProgressBar>();
        //gUIManager.SetEagleEyeCount(allowSwitchCount);
    }

    // Update is called once per frame
    void Update()
    {
        TopViewTimer();


        //check if the player has finished the training and is not in text
        if(gUIManager.introOver == true && gUIManager.inTextGUI == false)
        {
            if (!inTopView)
            {
                if (Input.GetKeyDown(KeyCode.E) && allowSwitchCount > 0)
                {
                    StartCoroutine(ShowTopView());
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("TopView inactive");
                    inTopView = false;
                    playerIndicator.gameObject.SetActive(false);
                    arrowIndicator.gameObject.SetActive(false);
                    //gUIManager.eagleEyeTimer.gameObject.SetActive(false);
                    //gUIManager.eagleText.SetText("E");
                    gUIManager.sprintIcon.gameObject.SetActive(true);
                    gUIManager.eagleIcon.texture = gUIManager.eagle;
                    gUIManager.progressBar.gameObject.SetActive(false);
                    progressBar.StopCountDown();
                    mainCamera.SetActive(true);
                    topCamera.SetActive(false);
                    timerController.TimerRestart();
                }
            }
        }

        
        


        
    }

    private IEnumerator ShowTopView()
    {
        SetEagleEyeActive();        
        yield return new WaitForSeconds(timeTopView);
        TopViewTimer();
        //StartCoroutine(TopViewTimer());
        SetEagleEyeInActive();
    }

    private void TopViewTimer()
    {
        timeLeft -= Time.deltaTime;
        gUIManager.SetEagleEyeTimer(timeLeft);
    }

    void SetEagleEyeActive()
    {
        Debug.Log("Topview active");
        inTopView = true;
        Debug.Log(inTopView.ToString());
        timeLeft = timeTopView;
        playerIndicator.gameObject.SetActive(true);
        arrowIndicator.gameObject.SetActive(true);
        allowSwitchCount--;
        gUIManager.SetEagleEyeCount(allowSwitchCount);
        mainCamera.SetActive(false);
        topCamera.SetActive(true);
        //gUIManager.eagleText.SetText("E");
        gUIManager.eagleIcon.texture = gUIManager.eaglePressed;
        gUIManager.sprintIcon.gameObject.SetActive(false);
        gUIManager.progressBar.gameObject.SetActive(true);
        progressBar.ActivateCountDown(11);
        //gUIManager.eagleEyeTimer.gameObject.SetActive(true);
        timerController.TimerStop();
        
    }

    void SetEagleEyeInActive()
    {
        Debug.Log("TopView inactive");
        inTopView = false;
        Debug.Log(inTopView.ToString());
        playerIndicator.gameObject.SetActive(false);
        arrowIndicator.gameObject.SetActive(false);
        //gUIManager.eagleEyeTimer.gameObject.SetActive(false);
        mainCamera.SetActive(true);
        topCamera.SetActive(false);
        //gUIManager.eagleText.SetText("E");
        gUIManager.sprintIcon.gameObject.SetActive(true);
        gUIManager.eagleIcon.texture = gUIManager.eagle;
        gUIManager.progressBar.gameObject.SetActive(false);
        progressBar.StopCountDown();
        timerController.TimerRestart();        
    }

    public void AddEagleEye()
    {
        allowSwitchCount++;
        gUIManager.SetEagleEyeCount(allowSwitchCount);
    }


}
