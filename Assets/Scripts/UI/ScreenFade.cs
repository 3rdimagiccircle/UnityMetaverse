using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public float fadeDuration = 1f;
    public Color fadeColor = Color.black;

    private Image fadeImage;

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadeImage.color = fadeColor;

        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, t);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, t);
            yield return null;
        }
    }
}
