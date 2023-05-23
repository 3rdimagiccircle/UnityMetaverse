using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class ChooseGameMode : MonoBehaviour
{
    //public Text titleNameText;
    public XRGeneralSettings xRGeneralSettings;

    private void Awake()
    {
        Debug.Log("Stopping XR...");

        xRGeneralSettings.Manager.StopSubsystems();
        xRGeneralSettings.Manager.DeinitializeLoader();
        Camera.main.fieldOfView = 60;
        Debug.Log("XR stopped completely.");
    }

    

    //public void ClickToChooseGameMode(int index)
    //{
    //    switch (index)
    //    {
    //        case 1:
    //            SceneManager.LoadScene(index);
    //            StartCoroutine(StartXR());
    //            break;
    //        case 2:
                
    //            SceneManager.LoadScene(index);
    //            StopXR();
    //            break;
    //    }
    //}

    public IEnumerator StartXRRoutine()
    {
        Debug.Log("Initializing XR...");
        yield return xRGeneralSettings.Manager.InitializeLoader();

        if (xRGeneralSettings.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            xRGeneralSettings.Manager.StartSubsystems();
        }
    }

    public void StartXR()
    {
        StartCoroutine(StartXRRoutine());
    }
    public void StopXR()
    {
        Debug.Log("Stopping XR...");

        xRGeneralSettings.Manager.StopSubsystems();
        xRGeneralSettings.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }

    public void BackToNativeApp()
    {
        Application.Unload();
    }

    public void CallNativeEvent(string titleName)
    {
        Debug.Log("Event calling from native--" + titleName);
        //titleNameText.text = titleName;
    }
}
