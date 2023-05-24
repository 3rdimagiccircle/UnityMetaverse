using System.Collections;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int degreesPerSecond = 200;
    public bool rotate = true;
    [SerializeField] private float rotationDuration = 5f;
    [SerializeField] private float pauseDuration = 2f;

    private Quaternion initialRotation;
    private float rotationTime = 0f;

    private void Start()
    {
        if (rotate)
        {
            initialRotation = transform.rotation;
            StartCoroutine(RotateObjectRoutine());
        }
    }

    private IEnumerator RotateObjectRoutine()
    {
        while (true)
        {
            if (rotate)
            {
                transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
            }

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
