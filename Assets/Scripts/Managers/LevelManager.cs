using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float sceneLoadTime;
    public GameObject loadScreen = null;
    private AsyncOperation asyncOperation;
    [SerializeField] private GameObject cardboardVRRig;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject playerController;
  
    private void Start()
    {
        if (cardboardVRRig != null )
        {
            if (GameManager.instance.VRMode)
            {
                SetRig(0);
            }
            else
            {
                SetRig(1);
            }
        }
    }


    public void GoToScene(string sceneName)
    {
        if (sceneName != null)
        {
            StartCoroutine(GoToSceneAsyncRoutine(sceneName));
            //loadingScreen.StartCoroutine(loadingScreen.HideLoadingScreenObjects());
        }
        else
        {
            Debug.LogError("Scene not found: " + sceneName);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetRig(int mode)
    {
        switch (mode)
        {
            case 0:
                GetLoadScreen(cardboardVRRig);

                break;

            case 1:
                GetLoadScreen(gameUI);

                break;
        }
    }

    private void GetLoadScreen(GameObject rig)
    {
        Transform loadScreenTransform = rig.transform.Find("LoadScreen");
        if (loadScreenTransform != null)
        {
            loadScreen = loadScreenTransform.gameObject;
            loadScreen.SetActive(false);
        }
    }

    private int CheckSceneBuildIndex()
    {
        int num = SceneManager.GetActiveScene().buildIndex;
        return num;
    }
    IEnumerator GoToSceneAsyncRoutine(string sceneName)
    {
        
            //loadScreen.SetActive(true);
            asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            yield return new WaitForSeconds(sceneLoadTime);

            //loadScreen.SetActive(false);

            yield break;
    }
}
