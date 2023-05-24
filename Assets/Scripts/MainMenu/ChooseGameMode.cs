using System.Collections;
using UnityEngine;
using UnityEngine.XR.Management;

public class ChooseGameMode : MonoBehaviour
{
    public XRGeneralSettings xRGeneralSettings;

    private void Awake()
    {
        if (xRGeneralSettings.Manager.isInitializationComplete)
        {
            StopXR();
        }
        else
        {
            StartCoroutine(WaitForInitializationAndStopXR());
        }

        SetCameraFieldOfView(60);
    }


    public void StartXRButton()
    {
        if (xRGeneralSettings.Manager.isInitializationComplete)
        {
            StartXR();
        }
        else
        {
            StartCoroutine(WaitForInitializationStartXR());
        }
    }

    public void StopXRButton()
    {
        if (xRGeneralSettings.Manager.isInitializationComplete)
        {
            StopXR();
        }
        else
        {
            StartCoroutine(WaitForInitializationAndStopXR());
        }
    }
    public void BackToNativeApp()
    {
        Application.Unload();
    }

    public void CallNativeEvent(string titleName)
    {
        Debug.Log("Event calling from native--" + titleName);
    }

    private IEnumerator WaitForInitializationAndStopXR()
    {
        while (!xRGeneralSettings.Manager.isInitializationComplete)
        {
            yield return null;
        }

        StopXR();
        yield break;
    }

    private IEnumerator WaitForInitializationStartXR()
    {
        while (!xRGeneralSettings.Manager.isInitializationComplete)
        {
            yield return null;
        }

        StartXR();
        yield break;
    }

    private void StartXR()
    {
        Debug.Log("Starting XR...");
        xRGeneralSettings.Manager.StartSubsystems();
    }

    private void StopXR()
    {
        Debug.Log("Stopping XR...");
        xRGeneralSettings.Manager.StopSubsystems();
        xRGeneralSettings.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
  
    private void SetCameraFieldOfView(float fieldOfView)
    {
        Camera.main.fieldOfView = fieldOfView;
    }
}
