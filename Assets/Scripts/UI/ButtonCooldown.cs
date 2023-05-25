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
            // Perform the button action or function here
            // ...

            // Start the cooldown
            StartCooldown();
        }
    }
    void StartCooldown()
    {
        currentCooldown = cooldownTime;
        isCooldown = true;
        button.interactable = false; // Disable the button during cooldown

        // Start the cooldown coroutine
        StartCoroutine(CooldownCoroutine());
    }
    IEnumerator CooldownCoroutine()
    {
        while (currentCooldown > 0)
        {
            yield return null; // Wait for the next frame
            currentCooldown -= Time.deltaTime; // Decrease the cooldown timer
        }

        // Cooldown finished
        isCooldown = false;
        button.interactable = true; // Enable the button

        yield return null;
    }

}
