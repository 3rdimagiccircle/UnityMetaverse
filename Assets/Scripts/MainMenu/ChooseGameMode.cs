using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class ChooseGameMode : MonoBehaviour
{
    public XRGeneralSettings xRGeneralSettings;
    [SerializeField] private Button pCButton;
    [SerializeField] private Button cardboardButton;
    [SerializeField] private Button phoneButton;

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

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android) 
        {
            pCButton.interactable = false;
        }
        else
        {
            cardboardButton.interactable = false;
            phoneButton.interactable = false;
        }
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
