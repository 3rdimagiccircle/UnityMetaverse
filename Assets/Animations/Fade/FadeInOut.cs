using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float fadeSpeed;
    public GameObject canvas;

    private Color plaqueColor;
    private CanvasGroup canvasGroup;
    private Renderer plaqueRenderer;
    
                                                              //Drag and drop in
    //this script goes on the top most game object that contains a [canvas] with a canvas group component and UI related children on that canvas.
    private void Awake()
    {
        //Debug.Log("Your UI container is fading in");

        //compontent on the main canvas to group together children alpha values
        canvasGroup = canvas.GetComponent<CanvasGroup>();

        //grabbing alpha value of main game object parent containing other elements (script should be on this object)
        plaqueRenderer = gameObject.GetComponent<Renderer>();
        plaqueColor = plaqueRenderer.material.color;


        StartCoroutine(FadeInObject(fadeSpeed));
    }
   
    private IEnumerator FadeInObject(float timeSpeed)
    {
        while (canvasGroup.alpha < 1.0f)
        {
            canvasGroup.alpha += timeSpeed;
            plaqueRenderer.material.color = new Color(plaqueRenderer.material.color.r, plaqueRenderer.material.color.g, plaqueRenderer.material.color.b, plaqueRenderer.material.color.a + timeSpeed);
            yield return new WaitForEndOfFrame();
;
        }
        yield break;
    }

}
    