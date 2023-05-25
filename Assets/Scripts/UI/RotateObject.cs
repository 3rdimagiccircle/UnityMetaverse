using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int degreesPerSecond = 200;
    public bool rotateVertical = true;
    public bool rotateHorizontal = false;
    [SerializeField] private float rotationDuration = 5f;
    [SerializeField] private float pauseDuration = 2f;

    private Quaternion initialRotation;
    private float rotationTime = 0f;

    private void Start()
    {
        if (rotateVertical)
        {
            initialRotation = transform.rotation;
            StartCoroutine(RotateObjectRoutine());
        }
        else
        {
            initialRotation = transform.rotation;
            StartCoroutine(RotateObjectHorizontalRoutine());
        }
    }

    private IEnumerator RotateObjectRoutine()
    {
        while (true)
        {
           
            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);

            rotationTime += Time.deltaTime;

            if (rotationTime >= rotationDuration)
            {
                rotationTime = 0f;
                transform.rotation = initialRotation;
                yield return new WaitForSeconds(pauseDuration);
            }

            yield return null;
        }
    }

    private IEnumerator RotateObjectHorizontalRoutine()
    {
        while (true)
        {
            transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);

            rotationTime += Time.deltaTime;

            if (rotationTime >= rotationDuration)
            {
                rotationTime = 0f;
                transform.rotation = initialRotation;
                yield return new WaitForSeconds(pauseDuration);
            }

            yield return null;
        }
    }
}
