using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EEProgressBar : MonoBehaviour
{
    public bool IsActive = false;
    private float indicatorTimer ;
    private float maxIndicatorTimer ;
    private Image radialProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        radialProgressBar = GameObject.Find("GUIManager").GetComponent<GUIManager>().progressBar;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            indicatorTimer -= Time.deltaTime;
            radialProgressBar.fillAmount = indicatorTimer / maxIndicatorTimer;
        }

        if (indicatorTimer < 0)
        {
            StopCountDown();
        }
    }

    public void ActivateCountDown(int countDownTime)
    {
        IsActive = true;
        maxIndicatorTimer = countDownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    public void StopCountDown()
    {
        IsActive = false;
    }
}
