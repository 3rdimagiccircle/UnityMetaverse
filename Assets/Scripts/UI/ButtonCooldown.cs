using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{
    public Button button;
    public float cooldownTime = 2f;

    private float currentCooldown = 0f;
    private bool isCooldown = false;

    void Start()
    {
        StartCooldown();
        button.onClick.AddListener(ButtonClick);
    }
    void ButtonClick()
    {
        if (!isCooldown)
        {
            StartCooldown();
        }
    }
    void StartCooldown()
    {
        currentCooldown = cooldownTime;
        isCooldown = true;
        button.interactable = false; 

        StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        while (currentCooldown > 0)
        {
            yield return null; 
            currentCooldown -= Time.deltaTime; 
        }

        isCooldown = false;
        button.interactable = true; 

        yield return null;
    }

}
