//using FlutterUnityIntegration;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private bool vRMode = false;

    public bool VRMode 
    {
        get { return vRMode; }
        set { vRMode = value; }
    }
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //gameObject.AddComponent<UnityMessageManager>();
    }

    public void VRModeButtonTrue()
    {
        VRMode = true;
    }

    //void HandleWebFnCall(String action)
    //{
    //    switch (action)
    //    {
    //        case "pause":
    //            Time.timeScale = 0;
    //            break;
    //        case "resume":
    //            Time.timeScale = 1;
    //            break;
    //        case "unload":
    //            Application.Unload();
    //            break;
    //        case "quit":
    //            Application.Quit();
    //            break;
    //    }
    //}
}