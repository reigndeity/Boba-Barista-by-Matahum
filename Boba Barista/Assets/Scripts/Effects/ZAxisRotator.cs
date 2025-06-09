using UnityEngine;

public class ZAxisRotator : MonoBehaviour
{
    public float minZRotation = -15f;   // Minimum Z angle
    public float maxZRotation = 15f;    // Maximum Z angle
    public float rotationSpeed = 1f;    // Speed of rotation
    public bool loop = false;           // Enable for continuous oscillation

    private Quaternion startRotation;
    private bool isRotating = false;

    void Start()
    {
        startRotation = transform.localRotation;

        if (loop)
            StartCoroutine(RotateZLoop());
    }

    public void TriggerRotation()
    {
        if (!isRotating)
            StartCoroutine(RotateZOnce());
    }

    private System.Collections.IEnumerator RotateZOnce()
    {
        isRotating = true;

        float timer = 0f;
        float duration = 1f / rotationSpeed;
        Quaternion from = Quaternion.Euler(0, 0, minZRotation);
        Quaternion to = Quaternion.Euler(0, 0, maxZRotation);

        // Rotate from min to max
        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(from, to, t);
            timer += Time.deltaTime;
            yield return null;
        }

        // Rotate back to original
        timer = 0f;
        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(to, startRotation, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = startRotation;
        isRotating = false;
    }

    private System.Collections.IEnumerator RotateZLoop()
    {
        isRotating = true;

        while (loop)
        {
            yield return RotateBetweenAngles(minZRotation, maxZRotation);
            yield return RotateBetweenAngles(maxZRotation, minZRotation);
        }

        isRotating = false;
    }

    private System.Collections.IEnumerator RotateBetweenAngles(float fromZ, float toZ)
    {
        float timer = 0f;
        float duration = 1f / rotationSpeed;
        Quaternion from = Quaternion.Euler(0, 0, fromZ);
        Quaternion to = Quaternion.Euler(0, 0, toZ);

        while (timer < duration)
        {
            float t = timer / duration;
            transform.localRotation = Quaternion.Lerp(from, to, t);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = to;
    }
}
