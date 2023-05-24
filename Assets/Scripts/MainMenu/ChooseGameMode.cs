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

    private IEnumerator WaitForInitializationAndStopXR()
    {
        while (!xRGeneralSettings.Manager.isInitializationComplete)
        {
            yield return null;
        }

        StopXR();
        yield break;
    }

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
            StartXR();
        }
    }

    public void StartXR()
    {
        Debug.Log("Starting XR...");
        xRGeneralSettings.Manager.StartSubsystems();
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
    }

    private void SetCameraFieldOfView(float fieldOfView)
    {
        Camera.main.fieldOfView = fieldOfView;
    }
}
