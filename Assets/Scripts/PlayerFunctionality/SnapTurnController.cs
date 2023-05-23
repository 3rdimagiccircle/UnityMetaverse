using UnityEngine;

public class SnapTurnController : MonoBehaviour
{
    public float snapAngle = 45f;  // The angle by which the player will snap-turn in degrees
    public float cooldownTime = 0.5f;  // Cooldown time in seconds
    public float snapTurnSpeed = 360f;  // Speed of the snap turn in degrees per second
    public string horizontalAxis = "Horizontal";  // Name of the joystick horizontal axis

    private bool isTurning = false;
    private float targetRotation = 0f;
    private float cooldownTimer = 0f;

    void Update()
    {
        // Check for joystick input if not in cooldown
        if (cooldownTimer <= 0f)
        {
            float input = Input.GetAxis(horizontalAxis);

            if (!isTurning && input != 0f)
            {
                StartTurn(input);
            }
        }
        else
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Perform the turn
        if (isTurning)
        {
            float step = snapTurnSpeed * Time.deltaTime;
            float currentRotation = transform.eulerAngles.y;
            float rotationRemaining = Mathf.DeltaAngle(currentRotation, targetRotation);

            if (Mathf.Abs(rotationRemaining) <= step)
            {
                transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
                isTurning = false;
                cooldownTimer = cooldownTime;
            }
            else
            {
                float newRotation = Mathf.MoveTowardsAngle(currentRotation, targetRotation, step);
                transform.rotation = Quaternion.Euler(0f, newRotation, 0f);
            }
        }
    }

    void StartTurn(float input)
    {
        // Calculate the target rotation
        targetRotation = transform.eulerAngles.y + (snapAngle * Mathf.Sign(input));

        // Ensure the target rotation stays within 0-360 degrees
        if (targetRotation >= 360f)
        {
            targetRotation -= 360f;
        }
        else if (targetRotation < 0f)
        {
            targetRotation += 360f;
        }

        isTurning = true;
    }
}
