using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRGazeTimer : MonoBehaviour
{
    public Image imgCircle;
    public float totalTime = 2;
    bool gvrStatus;
    public float gvrTimer;

    public UnityEvent gvrClick;

    private void Start()
    {
        GameObject imgCircleOb = GameObject.Find("loadingImage");
        imgCircle = imgCircleOb.GetComponent<Image>();
    }
    void Update()
    {
        if (gvrStatus && gvrTimer <= 2)
        {
            gvrTimer += Time.deltaTime;
            imgCircle.fillAmount = gvrTimer / totalTime;
            if (gvrTimer >= totalTime)
            {
                //print("call click");
                gvrClick.Invoke();
            }
        }
        else
        {
            gvrTimer = 0;
        }

        
    }

    public void Enter()
    {
        gvrStatus = true;

    }


    public void Exit()
    {
        gvrStatus = false;
        gvrTimer = 0;
        imgCircle.fillAmount = 0;

    }
}
