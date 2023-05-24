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

    public void GoToScene(string sceneName)
    {
        if (sceneName != null)
        {
            StartCoroutine(GoToSceneAsyncRoutine(sceneName));
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

    private int CheckSceneBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator GoToSceneAsyncRoutine(string sceneName)
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(sceneLoadTime);

        yield break;
    }
}
